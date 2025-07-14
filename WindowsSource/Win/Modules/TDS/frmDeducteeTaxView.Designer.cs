namespace ACPP.Modules.TDS
{
    partial class frmDeducteeTaxView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeducteeTaxView));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.lcDeducteeTDS = new DevExpress.XtraLayout.LayoutControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gcDeduteeDetails = new DevExpress.XtraGrid.GridControl();
            this.bandedgvDeducteeType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colbandNOP = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rglkpNOP = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rcolNOPId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcolNOP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colbandApplicableFrom = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colbandPolicyId = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colbandTaxType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colbandTDSRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rtxtRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colbandTDSExemptionLimit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rtxtExemption = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colbandTDSWithoutPan = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rtxtTDSWithoutPan = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colbandTDSExemptionLimitWithoutPan = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rtxtExemptionWithoutPan = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colbandSurchargeRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rtxtSurchargeRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colbandSurchargeExcemption = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rtxtSurchargeExcemption = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colbandEdCessRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rtxtEdCessRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colbandEdCessExemptionLimit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rtxtEdCessExemptionLimit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colbandSecEdCessRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rtxtSecEdCessRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colbandSecEdCessExemption = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rtxtSecEdCessExemption = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDelete = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rmeNOP = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.advBandedgvDeducteeType = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colNOP = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colApplicableFrom = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colTDSRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTDSExemption = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPolicyId = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTaxType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSurchargeRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSurchargeRateEexmptionLimit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colEdCessRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colEdCessEexmptionLimit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSecCessEdRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSecCeesEdcEexmptionLimit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.glkpDeducteeType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDeducteeTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblStatus = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDeducteeStatus = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem4 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblResidentStatus = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem6 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.ucDeducteeTDS = new Bosco.Utility.Controls.ucToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.lcDeducteeTDS)).BeginInit();
            this.lcDeducteeTDS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDeduteeDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedgvDeducteeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpNOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtExemption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtTDSWithoutPan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtExemptionWithoutPan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtSurchargeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtSurchargeExcemption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtEdCessRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtEdCessExemptionLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtSecEdCessRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtSecEdCessExemption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmeNOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedgvDeducteeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpDeducteeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDeducteeStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResidentStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // lcDeducteeTDS
            // 
            this.lcDeducteeTDS.Controls.Add(this.lblRecordCount);
            this.lcDeducteeTDS.Controls.Add(this.btnSave);
            this.lcDeducteeTDS.Controls.Add(this.btnCancel);
            this.lcDeducteeTDS.Controls.Add(this.gcDeduteeDetails);
            this.lcDeducteeTDS.Controls.Add(this.glkpDeducteeType);
            resources.ApplyResources(this.lcDeducteeTDS, "lcDeducteeTDS");
            this.lcDeducteeTDS.Name = "lcDeducteeTDS";
            this.lcDeducteeTDS.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(242, 265, 250, 350);
            this.lcDeducteeTDS.Root = this.layoutControlGroup1;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.StyleController = this.lcDeducteeTDS;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcDeducteeTDS;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.StyleController = this.lcDeducteeTDS;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gcDeduteeDetails
            // 
            resources.ApplyResources(this.gcDeduteeDetails, "gcDeduteeDetails");
            this.gcDeduteeDetails.MainView = this.bandedgvDeducteeType;
            this.gcDeduteeDetails.Name = "gcDeduteeDetails";
            this.gcDeduteeDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtRate,
            this.rtxtExemption,
            this.rDelete,
            this.rtxtSurchargeRate,
            this.rtxtSurchargeExcemption,
            this.rtxtEdCessRate,
            this.rtxtEdCessExemptionLimit,
            this.rtxtSecEdCessRate,
            this.rtxtSecEdCessExemption,
            this.rmeNOP,
            this.rglkpNOP,
            this.rtxtTDSWithoutPan,
            this.rtxtExemptionWithoutPan});
            this.gcDeduteeDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedgvDeducteeType,
            this.advBandedgvDeducteeType,
            this.gridView1});
            // 
            // bandedgvDeducteeType
            // 
            this.bandedgvDeducteeType.Appearance.FocusedCell.BackColor = ((System.Drawing.Color)(resources.GetObject("bandedgvDeducteeType.Appearance.FocusedCell.BackColor")));
            this.bandedgvDeducteeType.Appearance.FocusedCell.Font = ((System.Drawing.Font)(resources.GetObject("bandedgvDeducteeType.Appearance.FocusedCell.Font")));
            this.bandedgvDeducteeType.Appearance.FocusedCell.Options.UseBackColor = true;
            this.bandedgvDeducteeType.Appearance.FocusedCell.Options.UseFont = true;
            this.bandedgvDeducteeType.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("bandedgvDeducteeType.Appearance.FocusedRow.Font")));
            this.bandedgvDeducteeType.Appearance.FocusedRow.Options.UseFont = true;
            this.bandedgvDeducteeType.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("bandedgvDeducteeType.Appearance.HeaderPanel.Font")));
            this.bandedgvDeducteeType.Appearance.HeaderPanel.Options.UseFont = true;
            this.bandedgvDeducteeType.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.bandedgvDeducteeType.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bandedgvDeducteeType.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand4,
            this.gridBand5});
            this.bandedgvDeducteeType.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colbandPolicyId,
            this.colbandNOP,
            this.colbandApplicableFrom,
            this.colbandTaxType,
            this.colbandTDSRate,
            this.colbandTDSExemptionLimit,
            this.colbandTDSWithoutPan,
            this.colbandTDSExemptionLimitWithoutPan,
            this.colbandSurchargeRate,
            this.colbandSurchargeExcemption,
            this.colbandEdCessRate,
            this.colbandEdCessExemptionLimit,
            this.colbandSecEdCessRate,
            this.colbandSecEdCessExemption,
            this.colDelete});
            this.bandedgvDeducteeType.GridControl = this.gcDeduteeDetails;
            this.bandedgvDeducteeType.Name = "bandedgvDeducteeType";
            this.bandedgvDeducteeType.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.bandedgvDeducteeType.OptionsBehavior.AutoExpandAllGroups = true;
            this.bandedgvDeducteeType.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.bandedgvDeducteeType.OptionsView.AllowCellMerge = true;
            this.bandedgvDeducteeType.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.bandedgvDeducteeType.OptionsView.RowAutoHeight = true;
            this.bandedgvDeducteeType.OptionsView.ShowGroupPanel = false;
            this.bandedgvDeducteeType.OptionsView.ShowIndicator = false;
            this.bandedgvDeducteeType.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.bandedgvDeducteeType_RowClick);
            this.bandedgvDeducteeType.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.bandedgvDeducteeType_InitNewRow);
            this.bandedgvDeducteeType.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.bandedgvDeducteeType_InvalidRowException);
            this.bandedgvDeducteeType.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.bandedgvDeducteeType_ValidateRow);
            this.bandedgvDeducteeType.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.bandedgvDeducteeType_RowUpdated);
            // 
            // gridBand4
            // 
            resources.ApplyResources(this.gridBand4, "gridBand4");
            this.gridBand4.Columns.Add(this.colbandNOP);
            this.gridBand4.OptionsBand.ShowCaption = false;
            this.gridBand4.VisibleIndex = 0;
            // 
            // colbandNOP
            // 
            this.colbandNOP.AppearanceCell.Options.UseTextOptions = true;
            this.colbandNOP.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colbandNOP, "colbandNOP");
            this.colbandNOP.ColumnEdit = this.rglkpNOP;
            this.colbandNOP.FieldName = "NATURE_PAY_ID";
            this.colbandNOP.Name = "colbandNOP";
            this.colbandNOP.OptionsColumn.AllowEdit = false;
            this.colbandNOP.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            // 
            // rglkpNOP
            // 
            resources.ApplyResources(this.rglkpNOP, "rglkpNOP");
            this.rglkpNOP.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpNOP.Buttons"))))});
            this.rglkpNOP.Name = "rglkpNOP";
            this.rglkpNOP.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.rglkpNOP.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.rcolNOPId,
            this.rcolNOP});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // rcolNOPId
            // 
            resources.ApplyResources(this.rcolNOPId, "rcolNOPId");
            this.rcolNOPId.FieldName = "NATURE_PAY_ID";
            this.rcolNOPId.Name = "rcolNOPId";
            this.rcolNOPId.OptionsColumn.ShowCaption = false;
            // 
            // rcolNOP
            // 
            this.rcolNOP.AppearanceCell.Options.UseTextOptions = true;
            this.rcolNOP.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.rcolNOP.AppearanceHeader.Options.UseTextOptions = true;
            this.rcolNOP.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.rcolNOP, "rcolNOP");
            this.rcolNOP.ColumnEdit = this.repositoryItemMemoEdit1;
            this.rcolNOP.FieldName = "NAME";
            this.rcolNOP.Name = "rcolNOP";
            this.rcolNOP.OptionsColumn.AllowEdit = false;
            // 
            // gridBand5
            // 
            resources.ApplyResources(this.gridBand5, "gridBand5");
            this.gridBand5.Columns.Add(this.colbandApplicableFrom);
            this.gridBand5.OptionsBand.ShowCaption = false;
            this.gridBand5.VisibleIndex = 1;
            // 
            // colbandApplicableFrom
            // 
            this.colbandApplicableFrom.AppearanceCell.Options.UseTextOptions = true;
            this.colbandApplicableFrom.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colbandApplicableFrom.AppearanceHeader.Options.UseTextOptions = true;
            this.colbandApplicableFrom.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colbandApplicableFrom, "colbandApplicableFrom");
            this.colbandApplicableFrom.FieldName = "APPLICABLE_FROM";
            this.colbandApplicableFrom.Name = "colbandApplicableFrom";
            this.colbandApplicableFrom.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colbandPolicyId
            // 
            resources.ApplyResources(this.colbandPolicyId, "colbandPolicyId");
            this.colbandPolicyId.FieldName = "NATURE_PAY_ID";
            this.colbandPolicyId.Name = "colbandPolicyId";
            // 
            // colbandTaxType
            // 
            resources.ApplyResources(this.colbandTaxType, "colbandTaxType");
            this.colbandTaxType.FieldName = "TAX_TYPE_NAME";
            this.colbandTaxType.Name = "colbandTaxType";
            // 
            // colbandTDSRate
            // 
            this.colbandTDSRate.AppearanceCell.Options.UseTextOptions = true;
            this.colbandTDSRate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colbandTDSRate, "colbandTDSRate");
            this.colbandTDSRate.ColumnEdit = this.rtxtRate;
            this.colbandTDSRate.FieldName = "TDS_RATE";
            this.colbandTDSRate.Name = "colbandTDSRate";
            this.colbandTDSRate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtRate
            // 
            resources.ApplyResources(this.rtxtRate, "rtxtRate");
            this.rtxtRate.DisplayFormat.FormatString = "N";
            this.rtxtRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtRate.Mask.EditMask = resources.GetString("rtxtRate.Mask.EditMask");
            this.rtxtRate.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtRate.Mask.MaskType")));
            this.rtxtRate.Name = "rtxtRate";
            // 
            // colbandTDSExemptionLimit
            // 
            this.colbandTDSExemptionLimit.AppearanceCell.Options.UseTextOptions = true;
            this.colbandTDSExemptionLimit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colbandTDSExemptionLimit, "colbandTDSExemptionLimit");
            this.colbandTDSExemptionLimit.ColumnEdit = this.rtxtExemption;
            this.colbandTDSExemptionLimit.FieldName = "TDS_EXEMPTION_LIMIT";
            this.colbandTDSExemptionLimit.Name = "colbandTDSExemptionLimit";
            this.colbandTDSExemptionLimit.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtExemption
            // 
            resources.ApplyResources(this.rtxtExemption, "rtxtExemption");
            this.rtxtExemption.DisplayFormat.FormatString = "N";
            this.rtxtExemption.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtExemption.Mask.EditMask = resources.GetString("rtxtExemption.Mask.EditMask");
            this.rtxtExemption.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtExemption.Mask.MaskType")));
            this.rtxtExemption.Name = "rtxtExemption";
            // 
            // colbandTDSWithoutPan
            // 
            resources.ApplyResources(this.colbandTDSWithoutPan, "colbandTDSWithoutPan");
            this.colbandTDSWithoutPan.ColumnEdit = this.rtxtTDSWithoutPan;
            this.colbandTDSWithoutPan.FieldName = "TDSRATE_WITHOUT_PAN";
            this.colbandTDSWithoutPan.Name = "colbandTDSWithoutPan";
            this.colbandTDSWithoutPan.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtTDSWithoutPan
            // 
            resources.ApplyResources(this.rtxtTDSWithoutPan, "rtxtTDSWithoutPan");
            this.rtxtTDSWithoutPan.DisplayFormat.FormatString = "N";
            this.rtxtTDSWithoutPan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtTDSWithoutPan.Mask.EditMask = resources.GetString("rtxtTDSWithoutPan.Mask.EditMask");
            this.rtxtTDSWithoutPan.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtTDSWithoutPan.Mask.MaskType")));
            this.rtxtTDSWithoutPan.Name = "rtxtTDSWithoutPan";
            // 
            // colbandTDSExemptionLimitWithoutPan
            // 
            resources.ApplyResources(this.colbandTDSExemptionLimitWithoutPan, "colbandTDSExemptionLimitWithoutPan");
            this.colbandTDSExemptionLimitWithoutPan.ColumnEdit = this.rtxtExemptionWithoutPan;
            this.colbandTDSExemptionLimitWithoutPan.FieldName = "TDSEXEMPTION_LIMIT_WITHOUT_PAN";
            this.colbandTDSExemptionLimitWithoutPan.Name = "colbandTDSExemptionLimitWithoutPan";
            this.colbandTDSExemptionLimitWithoutPan.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtExemptionWithoutPan
            // 
            resources.ApplyResources(this.rtxtExemptionWithoutPan, "rtxtExemptionWithoutPan");
            this.rtxtExemptionWithoutPan.DisplayFormat.FormatString = "N";
            this.rtxtExemptionWithoutPan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtExemptionWithoutPan.Mask.EditMask = resources.GetString("rtxtExemptionWithoutPan.Mask.EditMask");
            this.rtxtExemptionWithoutPan.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtExemptionWithoutPan.Mask.MaskType")));
            this.rtxtExemptionWithoutPan.Name = "rtxtExemptionWithoutPan";
            // 
            // colbandSurchargeRate
            // 
            this.colbandSurchargeRate.AppearanceCell.Options.UseTextOptions = true;
            this.colbandSurchargeRate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colbandSurchargeRate, "colbandSurchargeRate");
            this.colbandSurchargeRate.ColumnEdit = this.rtxtSurchargeRate;
            this.colbandSurchargeRate.FieldName = "SUR_RATE";
            this.colbandSurchargeRate.Name = "colbandSurchargeRate";
            this.colbandSurchargeRate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtSurchargeRate
            // 
            resources.ApplyResources(this.rtxtSurchargeRate, "rtxtSurchargeRate");
            this.rtxtSurchargeRate.DisplayFormat.FormatString = "N";
            this.rtxtSurchargeRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtSurchargeRate.Mask.EditMask = resources.GetString("rtxtSurchargeRate.Mask.EditMask");
            this.rtxtSurchargeRate.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtSurchargeRate.Mask.MaskType")));
            this.rtxtSurchargeRate.Name = "rtxtSurchargeRate";
            // 
            // colbandSurchargeExcemption
            // 
            this.colbandSurchargeExcemption.AppearanceCell.Options.UseTextOptions = true;
            this.colbandSurchargeExcemption.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colbandSurchargeExcemption, "colbandSurchargeExcemption");
            this.colbandSurchargeExcemption.ColumnEdit = this.rtxtSurchargeExcemption;
            this.colbandSurchargeExcemption.FieldName = "SUR_EXEMPTION";
            this.colbandSurchargeExcemption.Name = "colbandSurchargeExcemption";
            this.colbandSurchargeExcemption.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtSurchargeExcemption
            // 
            resources.ApplyResources(this.rtxtSurchargeExcemption, "rtxtSurchargeExcemption");
            this.rtxtSurchargeExcemption.DisplayFormat.FormatString = "N";
            this.rtxtSurchargeExcemption.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtSurchargeExcemption.Mask.EditMask = resources.GetString("rtxtSurchargeExcemption.Mask.EditMask");
            this.rtxtSurchargeExcemption.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtSurchargeExcemption.Mask.MaskType")));
            this.rtxtSurchargeExcemption.Name = "rtxtSurchargeExcemption";
            // 
            // colbandEdCessRate
            // 
            this.colbandEdCessRate.AppearanceCell.Options.UseTextOptions = true;
            this.colbandEdCessRate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colbandEdCessRate, "colbandEdCessRate");
            this.colbandEdCessRate.ColumnEdit = this.rtxtEdCessRate;
            this.colbandEdCessRate.FieldName = "ED_CESS_RATE";
            this.colbandEdCessRate.Name = "colbandEdCessRate";
            this.colbandEdCessRate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtEdCessRate
            // 
            resources.ApplyResources(this.rtxtEdCessRate, "rtxtEdCessRate");
            this.rtxtEdCessRate.DisplayFormat.FormatString = "N";
            this.rtxtEdCessRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtEdCessRate.Mask.EditMask = resources.GetString("rtxtEdCessRate.Mask.EditMask");
            this.rtxtEdCessRate.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtEdCessRate.Mask.MaskType")));
            this.rtxtEdCessRate.Name = "rtxtEdCessRate";
            // 
            // colbandEdCessExemptionLimit
            // 
            this.colbandEdCessExemptionLimit.AppearanceCell.Options.UseTextOptions = true;
            this.colbandEdCessExemptionLimit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colbandEdCessExemptionLimit, "colbandEdCessExemptionLimit");
            this.colbandEdCessExemptionLimit.ColumnEdit = this.rtxtEdCessExemptionLimit;
            this.colbandEdCessExemptionLimit.FieldName = "ED_CESS_EXEMPTION";
            this.colbandEdCessExemptionLimit.Name = "colbandEdCessExemptionLimit";
            this.colbandEdCessExemptionLimit.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtEdCessExemptionLimit
            // 
            resources.ApplyResources(this.rtxtEdCessExemptionLimit, "rtxtEdCessExemptionLimit");
            this.rtxtEdCessExemptionLimit.DisplayFormat.FormatString = "N";
            this.rtxtEdCessExemptionLimit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtEdCessExemptionLimit.Mask.EditMask = resources.GetString("rtxtEdCessExemptionLimit.Mask.EditMask");
            this.rtxtEdCessExemptionLimit.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtEdCessExemptionLimit.Mask.MaskType")));
            this.rtxtEdCessExemptionLimit.Name = "rtxtEdCessExemptionLimit";
            // 
            // colbandSecEdCessRate
            // 
            this.colbandSecEdCessRate.AppearanceCell.Options.UseTextOptions = true;
            this.colbandSecEdCessRate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colbandSecEdCessRate, "colbandSecEdCessRate");
            this.colbandSecEdCessRate.ColumnEdit = this.rtxtSecEdCessRate;
            this.colbandSecEdCessRate.FieldName = "SEC_ED_CESS_RATE";
            this.colbandSecEdCessRate.Name = "colbandSecEdCessRate";
            this.colbandSecEdCessRate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtSecEdCessRate
            // 
            resources.ApplyResources(this.rtxtSecEdCessRate, "rtxtSecEdCessRate");
            this.rtxtSecEdCessRate.DisplayFormat.FormatString = "N";
            this.rtxtSecEdCessRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtSecEdCessRate.Mask.EditMask = resources.GetString("rtxtSecEdCessRate.Mask.EditMask");
            this.rtxtSecEdCessRate.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtSecEdCessRate.Mask.MaskType")));
            this.rtxtSecEdCessRate.Name = "rtxtSecEdCessRate";
            // 
            // colbandSecEdCessExemption
            // 
            this.colbandSecEdCessExemption.AppearanceCell.Options.UseTextOptions = true;
            this.colbandSecEdCessExemption.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colbandSecEdCessExemption, "colbandSecEdCessExemption");
            this.colbandSecEdCessExemption.ColumnEdit = this.rtxtSecEdCessExemption;
            this.colbandSecEdCessExemption.DisplayFormat.FormatString = "N";
            this.colbandSecEdCessExemption.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colbandSecEdCessExemption.FieldName = "SEC_ED_CESS_EXEMPTION";
            this.colbandSecEdCessExemption.Name = "colbandSecEdCessExemption";
            this.colbandSecEdCessExemption.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtSecEdCessExemption
            // 
            resources.ApplyResources(this.rtxtSecEdCessExemption, "rtxtSecEdCessExemption");
            this.rtxtSecEdCessExemption.DisplayFormat.FormatString = "N";
            this.rtxtSecEdCessExemption.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtSecEdCessExemption.Mask.EditMask = resources.GetString("rtxtSecEdCessExemption.Mask.EditMask");
            this.rtxtSecEdCessExemption.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtSecEdCessExemption.Mask.MaskType")));
            this.rtxtSecEdCessExemption.Name = "rtxtSecEdCessExemption";
            // 
            // colDelete
            // 
            resources.ApplyResources(this.colDelete, "colDelete");
            this.colDelete.ColumnEdit = this.rDelete;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDelete.OptionsColumn.FixedWidth = true;
            this.colDelete.OptionsColumn.ReadOnly = true;
            this.colDelete.OptionsColumn.ShowCaption = false;
            this.colDelete.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rDelete
            // 
            resources.ApplyResources(this.rDelete, "rDelete");
            this.rDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rDelete.Buttons"))), resources.GetString("rDelete.Buttons1"), ((int)(resources.GetObject("rDelete.Buttons2"))), ((bool)(resources.GetObject("rDelete.Buttons3"))), ((bool)(resources.GetObject("rDelete.Buttons4"))), ((bool)(resources.GetObject("rDelete.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rDelete.Buttons6"))), global::ACPP.Properties.Resources.Delete_Mob, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rDelete.Buttons7"), ((object)(resources.GetObject("rDelete.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rDelete.Buttons9"))), ((bool)(resources.GetObject("rDelete.Buttons10"))))});
            this.rDelete.Name = "rDelete";
            this.rDelete.Click += new System.EventHandler(this.rDelete_Click);
            // 
            // rmeNOP
            // 
            this.rmeNOP.Appearance.Options.UseTextOptions = true;
            this.rmeNOP.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.rmeNOP.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rmeNOP.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.rmeNOP.Name = "rmeNOP";
            this.rmeNOP.ReadOnly = true;
            // 
            // advBandedgvDeducteeType
            // 
            this.advBandedgvDeducteeType.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("advBandedgvDeducteeType.Appearance.HeaderPanel.Font")));
            this.advBandedgvDeducteeType.Appearance.HeaderPanel.Options.UseFont = true;
            this.advBandedgvDeducteeType.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand3});
            this.advBandedgvDeducteeType.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colPolicyId,
            this.colNOP,
            this.colApplicableFrom,
            this.colTaxType,
            this.colTDSRate,
            this.colTDSExemption,
            this.colSurchargeRate,
            this.colSurchargeRateEexmptionLimit,
            this.colEdCessRate,
            this.colEdCessEexmptionLimit,
            this.colSecCessEdRate,
            this.colSecCeesEdcEexmptionLimit});
            this.advBandedgvDeducteeType.GridControl = this.gcDeduteeDetails;
            this.advBandedgvDeducteeType.Name = "advBandedgvDeducteeType";
            this.advBandedgvDeducteeType.OptionsView.ShowGroupPanel = false;
            this.advBandedgvDeducteeType.OptionsView.ShowIndicator = false;
            // 
            // gridBand1
            // 
            resources.ApplyResources(this.gridBand1, "gridBand1");
            this.gridBand1.Columns.Add(this.colNOP);
            this.gridBand1.OptionsBand.ShowCaption = false;
            this.gridBand1.VisibleIndex = 0;
            // 
            // colNOP
            // 
            resources.ApplyResources(this.colNOP, "colNOP");
            this.colNOP.FieldName = "NAME";
            this.colNOP.Name = "colNOP";
            this.colNOP.OptionsColumn.AllowEdit = false;
            this.colNOP.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            // 
            // gridBand2
            // 
            resources.ApplyResources(this.gridBand2, "gridBand2");
            this.gridBand2.Columns.Add(this.colApplicableFrom);
            this.gridBand2.OptionsBand.ShowCaption = false;
            this.gridBand2.VisibleIndex = 1;
            // 
            // colApplicableFrom
            // 
            this.colApplicableFrom.AppearanceHeader.Options.UseTextOptions = true;
            this.colApplicableFrom.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colApplicableFrom, "colApplicableFrom");
            this.colApplicableFrom.FieldName = "APPLICABLE_FROM";
            this.colApplicableFrom.Name = "colApplicableFrom";
            this.colApplicableFrom.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gridBand3.AppearanceHeader.Font")));
            this.gridBand3.AppearanceHeader.Options.UseFont = true;
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.gridBand3, "gridBand3");
            this.gridBand3.Columns.Add(this.colTDSRate);
            this.gridBand3.Columns.Add(this.colTDSExemption);
            this.gridBand3.VisibleIndex = 2;
            // 
            // colTDSRate
            // 
            this.colTDSRate.AppearanceCell.Options.UseTextOptions = true;
            this.colTDSRate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colTDSRate, "colTDSRate");
            this.colTDSRate.ColumnEdit = this.rtxtRate;
            this.colTDSRate.FieldName = "TDS_RATE";
            this.colTDSRate.Name = "colTDSRate";
            // 
            // colTDSExemption
            // 
            this.colTDSExemption.AppearanceCell.Options.UseTextOptions = true;
            this.colTDSExemption.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colTDSExemption, "colTDSExemption");
            this.colTDSExemption.ColumnEdit = this.rtxtExemption;
            this.colTDSExemption.FieldName = "TDS_EXEMPTION_LIMIT";
            this.colTDSExemption.Name = "colTDSExemption";
            // 
            // colPolicyId
            // 
            resources.ApplyResources(this.colPolicyId, "colPolicyId");
            this.colPolicyId.FieldName = "TDS_POLICY_ID";
            this.colPolicyId.Name = "colPolicyId";
            // 
            // colTaxType
            // 
            resources.ApplyResources(this.colTaxType, "colTaxType");
            this.colTaxType.FieldName = "TAX_TYPE_NAME";
            this.colTaxType.Name = "colTaxType";
            // 
            // colSurchargeRate
            // 
            this.colSurchargeRate.AppearanceCell.Options.UseTextOptions = true;
            this.colSurchargeRate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colSurchargeRate, "colSurchargeRate");
            this.colSurchargeRate.FieldName = "TDS_RATE";
            this.colSurchargeRate.Name = "colSurchargeRate";
            // 
            // colSurchargeRateEexmptionLimit
            // 
            this.colSurchargeRateEexmptionLimit.AppearanceCell.Options.UseTextOptions = true;
            this.colSurchargeRateEexmptionLimit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colSurchargeRateEexmptionLimit, "colSurchargeRateEexmptionLimit");
            this.colSurchargeRateEexmptionLimit.FieldName = "TDS_EXEMPTION_LIMIT";
            this.colSurchargeRateEexmptionLimit.Name = "colSurchargeRateEexmptionLimit";
            // 
            // colEdCessRate
            // 
            this.colEdCessRate.AppearanceCell.Options.UseTextOptions = true;
            this.colEdCessRate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colEdCessRate, "colEdCessRate");
            this.colEdCessRate.FieldName = "TDS_RATE";
            this.colEdCessRate.Name = "colEdCessRate";
            // 
            // colEdCessEexmptionLimit
            // 
            this.colEdCessEexmptionLimit.AppearanceCell.Options.UseTextOptions = true;
            this.colEdCessEexmptionLimit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colEdCessEexmptionLimit, "colEdCessEexmptionLimit");
            this.colEdCessEexmptionLimit.FieldName = "TDS_EXEMPTION_LIMIT";
            this.colEdCessEexmptionLimit.Name = "colEdCessEexmptionLimit";
            // 
            // colSecCessEdRate
            // 
            this.colSecCessEdRate.AppearanceCell.Options.UseTextOptions = true;
            this.colSecCessEdRate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colSecCessEdRate, "colSecCessEdRate");
            this.colSecCessEdRate.FieldName = "TDS_RATE";
            this.colSecCessEdRate.Name = "colSecCessEdRate";
            // 
            // colSecCeesEdcEexmptionLimit
            // 
            this.colSecCeesEdcEexmptionLimit.AppearanceCell.Options.UseTextOptions = true;
            this.colSecCeesEdcEexmptionLimit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.colSecCeesEdcEexmptionLimit, "colSecCeesEdcEexmptionLimit");
            this.colSecCeesEdcEexmptionLimit.FieldName = "TDS_EXEMPTION_LIMIT";
            this.colSecCeesEdcEexmptionLimit.Name = "colSecCeesEdcEexmptionLimit";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gcDeduteeDetails;
            this.gridView1.Name = "gridView1";
            // 
            // glkpDeducteeType
            // 
            resources.ApplyResources(this.glkpDeducteeType, "glkpDeducteeType");
            this.glkpDeducteeType.Name = "glkpDeducteeType";
            this.glkpDeducteeType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpDeducteeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpDeducteeType.Properties.Buttons"))))});
            this.glkpDeducteeType.Properties.NullText = resources.GetString("glkpDeducteeType.Properties.NullText");
            this.glkpDeducteeType.Properties.View = this.gridLookUpEdit1View;
            this.glkpDeducteeType.StyleController = this.lcDeducteeTDS;
            this.glkpDeducteeType.EditValueChanged += new System.EventHandler(this.glkpDeducteeType_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDeducteeTypeId,
            this.colName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colDeducteeTypeId
            // 
            resources.ApplyResources(this.colDeducteeTypeId, "colDeducteeTypeId");
            this.colDeducteeTypeId.FieldName = "DEDUCTEE_TYPE_ID";
            this.colDeducteeTypeId.Name = "colDeducteeTypeId";
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.lblStatus,
            this.simpleLabelItem2,
            this.lblDeducteeStatus,
            this.simpleLabelItem4,
            this.lblResidentStatus,
            this.simpleLabelItem6,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.emptySpaceItem3,
            this.simpleLabelItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(979, 464);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.glkpDeducteeType;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.layoutControlItem5.Size = new System.Drawing.Size(409, 24);
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(73, 13);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AllowHotTrack = false;
            this.lblStatus.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblStatus.AppearanceItemCaption.Font")));
            this.lblStatus.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Location = new System.Drawing.Point(878, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 24);
            this.lblStatus.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblStatus.TextSize = new System.Drawing.Size(36, 13);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            resources.ApplyResources(this.simpleLabelItem2, "simpleLabelItem2");
            this.simpleLabelItem2.Location = new System.Drawing.Point(830, 0);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(48, 24);
            this.simpleLabelItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(31, 13);
            // 
            // lblDeducteeStatus
            // 
            this.lblDeducteeStatus.AllowHotTrack = false;
            this.lblDeducteeStatus.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblDeducteeStatus.AppearanceItemCaption.Font")));
            this.lblDeducteeStatus.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblDeducteeStatus, "lblDeducteeStatus");
            this.lblDeducteeStatus.Location = new System.Drawing.Point(702, 0);
            this.lblDeducteeStatus.Name = "lblDeducteeStatus";
            this.lblDeducteeStatus.Size = new System.Drawing.Size(128, 24);
            this.lblDeducteeStatus.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblDeducteeStatus.TextSize = new System.Drawing.Size(53, 13);
            // 
            // simpleLabelItem4
            // 
            this.simpleLabelItem4.AllowHotTrack = false;
            resources.ApplyResources(this.simpleLabelItem4, "simpleLabelItem4");
            this.simpleLabelItem4.Location = new System.Drawing.Point(598, 0);
            this.simpleLabelItem4.Name = "simpleLabelItem4";
            this.simpleLabelItem4.Size = new System.Drawing.Size(104, 24);
            this.simpleLabelItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem4.TextSize = new System.Drawing.Size(80, 13);
            // 
            // lblResidentStatus
            // 
            this.lblResidentStatus.AllowHotTrack = false;
            this.lblResidentStatus.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblResidentStatus.AppearanceItemCaption.Font")));
            this.lblResidentStatus.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblResidentStatus, "lblResidentStatus");
            this.lblResidentStatus.Location = new System.Drawing.Point(511, 0);
            this.lblResidentStatus.Name = "lblResidentStatus";
            this.lblResidentStatus.Size = new System.Drawing.Size(87, 24);
            this.lblResidentStatus.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblResidentStatus.TextSize = new System.Drawing.Size(50, 13);
            // 
            // simpleLabelItem6
            // 
            this.simpleLabelItem6.AllowHotTrack = false;
            resources.ApplyResources(this.simpleLabelItem6, "simpleLabelItem6");
            this.simpleLabelItem6.Location = new System.Drawing.Point(409, 0);
            this.simpleLabelItem6.Name = "simpleLabelItem6";
            this.simpleLabelItem6.Size = new System.Drawing.Size(102, 24);
            this.simpleLabelItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem6.TextSize = new System.Drawing.Size(76, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcDeduteeDetails;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(979, 412);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnCancel;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(908, 436);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(71, 28);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(837, 436);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(71, 28);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lblRecordCount;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(953, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2);
            this.layoutControlItem6.Size = new System.Drawing.Size(26, 24);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(919, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(34, 24);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem1.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.simpleLabelItem1, "simpleLabelItem1");
            this.simpleLabelItem1.Image = global::ACPP.Properties.Resources.info;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 436);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(837, 28);
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(311, 24);
            // 
            // ucDeducteeTDS
            // 
            this.ucDeducteeTDS.ChangeAddCaption = "&Add";
            this.ucDeducteeTDS.ChangeCaption = "&Edit";
            this.ucDeducteeTDS.ChangeDeleteCaption = "&Delete";
            this.ucDeducteeTDS.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucDeducteeTDS.ChangePrintCaption = "&Print";
            this.ucDeducteeTDS.DisableAddButton = true;
            this.ucDeducteeTDS.DisableCloseButton = true;
            this.ucDeducteeTDS.DisableDeleteButton = true;
            this.ucDeducteeTDS.DisableDownloadExcel = true;
            this.ucDeducteeTDS.DisableEditButton = true;
            this.ucDeducteeTDS.DisableMoveTransaction = true;
            this.ucDeducteeTDS.DisableNatureofPayments = true;
            this.ucDeducteeTDS.DisablePrintButton = true;
            this.ucDeducteeTDS.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucDeducteeTDS, "ucDeducteeTDS");
            this.ucDeducteeTDS.Name = "ucDeducteeTDS";
            this.ucDeducteeTDS.ShowHTML = true;
            this.ucDeducteeTDS.ShowMMT = true;
            this.ucDeducteeTDS.ShowPDF = true;
            this.ucDeducteeTDS.ShowRTF = true;
            this.ucDeducteeTDS.ShowText = true;
            this.ucDeducteeTDS.ShowXLS = true;
            this.ucDeducteeTDS.ShowXLSX = true;
            this.ucDeducteeTDS.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeTDS.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeTDS.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeTDS.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeTDS.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeTDS.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeTDS.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeTDS.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeTDS.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeTDS.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeTDS.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeTDS.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // frmDeducteeTaxView
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.lcDeducteeTDS);
            this.Name = "frmDeducteeTaxView";
            this.Load += new System.EventHandler(this.frmDeducteeTaxView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcDeducteeTDS)).EndInit();
            this.lcDeducteeTDS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDeduteeDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedgvDeducteeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpNOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtExemption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtTDSWithoutPan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtExemptionWithoutPan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtSurchargeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtSurchargeExcemption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtEdCessRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtEdCessExemptionLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtSecEdCessRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtSecEdCessExemption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmeNOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedgvDeducteeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpDeducteeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDeducteeStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResidentStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcDeducteeTDS;
        private DevExpress.XtraEditors.GridLookUpEdit glkpDeducteeType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Bosco.Utility.Controls.ucToolBar ucDeducteeTDS;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.SimpleLabelItem lblStatus;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.SimpleLabelItem lblDeducteeStatus;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem4;
        private DevExpress.XtraLayout.SimpleLabelItem lblResidentStatus;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colDeducteeTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.GridControl gcDeduteeDetails;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedgvDeducteeType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNOP;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colApplicableFrom;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTDSRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTDSExemption;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPolicyId;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTaxType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtExemption;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSurchargeRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSurchargeRateEexmptionLimit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colEdCessRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colEdCessEexmptionLimit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSecCessEdRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSecCeesEdcEexmptionLimit;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedgvDeducteeType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandNOP;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandApplicableFrom;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandTDSRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandTDSExemptionLimit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandTaxType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandSurchargeRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandSurchargeExcemption;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandEdCessRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandEdCessExemptionLimit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandSecEdCessRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandSecEdCessExemption;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandPolicyId;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rDelete;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtSurchargeRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtSurchargeExcemption;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtEdCessRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtEdCessExemptionLimit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtSecEdCessRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtSecEdCessExemption;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit rmeNOP;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpNOP;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn rcolNOPId;
        private DevExpress.XtraGrid.Columns.GridColumn rcolNOP;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandTDSWithoutPan;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtTDSWithoutPan;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colbandTDSExemptionLimitWithoutPan;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtExemptionWithoutPan;
    }
}