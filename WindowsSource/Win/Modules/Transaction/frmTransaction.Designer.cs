namespace ACPP.Modules.Transaction
{
    partial class frmTransaction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransaction));
            this.lcTransactionAdd = new DevExpress.XtraLayout.LayoutControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtNarration = new DevExpress.XtraEditors.TextEdit();
            this.lblCurreny = new DevExpress.XtraEditors.LabelControl();
            this.lblActualAmount = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtExchangeRate = new DevExpress.XtraEditors.TextEdit();
            this.txtReceiptAmount = new DevExpress.XtraEditors.TextEdit();
            this.glkpPurpose = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit3View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPurposeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPurposeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpReceiptType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colReceiptID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceiptType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpDonor = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDonorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dteReconciledOn = new DevExpress.XtraEditors.DateEdit();
            this.txtChequeNo = new DevExpress.XtraEditors.TextEdit();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.txtVoucher = new DevExpress.XtraEditors.TextEdit();
            this.glkpCostCentre = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDCostCenterID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAbbrevation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostCenterName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpCashBankLedger = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCashBankId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCashBankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpLedger = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rboTransactionType = new DevExpress.XtraEditors.RadioGroup();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dteTransactionDate = new DevExpress.XtraEditors.DateEdit();
            this.lcgTransaction = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTransactionType = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgTransactionGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblLedger = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCashBank = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCostCenter = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAmount = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDonor = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblPurpose = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblReceiptType = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblReceiptAmount = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblExchangeRate = new DevExpress.XtraLayout.LayoutControlItem();
            this.esiExchangeRate = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblReceiptActualAmount = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCurrencySymbol = new DevExpress.XtraLayout.LayoutControlItem();
            this.esiCurrenySymbol = new DevExpress.XtraLayout.EmptySpaceItem();
            this.esiActualAmount = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblNarration = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblChequeNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblReconciledOn = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblTransactionDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblVoucher = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcTransactionAdd)).BeginInit();
            this.lcTransactionAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPurpose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit3View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpReceiptType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpDonor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReconciledOn.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReconciledOn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChequeNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCostCentre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCashBankLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rboTransactionType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTransactionDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTransactionDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTransactionGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCashBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCostCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPurpose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReceiptType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReceiptAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExchangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiExchangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReceiptActualAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrencySymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiCurrenySymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiActualAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblChequeNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReconciledOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // lcTransactionAdd
            // 
            this.lcTransactionAdd.Controls.Add(this.btnSave);
            this.lcTransactionAdd.Controls.Add(this.txtNarration);
            this.lcTransactionAdd.Controls.Add(this.lblCurreny);
            this.lcTransactionAdd.Controls.Add(this.lblActualAmount);
            this.lcTransactionAdd.Controls.Add(this.btnClose);
            this.lcTransactionAdd.Controls.Add(this.txtExchangeRate);
            this.lcTransactionAdd.Controls.Add(this.txtReceiptAmount);
            this.lcTransactionAdd.Controls.Add(this.glkpPurpose);
            this.lcTransactionAdd.Controls.Add(this.glkpReceiptType);
            this.lcTransactionAdd.Controls.Add(this.glkpDonor);
            this.lcTransactionAdd.Controls.Add(this.dteReconciledOn);
            this.lcTransactionAdd.Controls.Add(this.txtChequeNo);
            this.lcTransactionAdd.Controls.Add(this.txtAmount);
            this.lcTransactionAdd.Controls.Add(this.txtVoucher);
            this.lcTransactionAdd.Controls.Add(this.glkpCostCentre);
            this.lcTransactionAdd.Controls.Add(this.glkpCashBankLedger);
            this.lcTransactionAdd.Controls.Add(this.glkpLedger);
            this.lcTransactionAdd.Controls.Add(this.rboTransactionType);
            this.lcTransactionAdd.Controls.Add(this.glkpProject);
            this.lcTransactionAdd.Controls.Add(this.dteTransactionDate);
            resources.ApplyResources(this.lcTransactionAdd, "lcTransactionAdd");
            this.lcTransactionAdd.Name = "lcTransactionAdd";
            this.lcTransactionAdd.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(530, 180, 250, 350);
            this.lcTransactionAdd.Root = this.lcgTransaction;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcTransactionAdd;
            // 
            // txtNarration
            // 
            resources.ApplyResources(this.txtNarration, "txtNarration");
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtNarration.StyleController = this.lcTransactionAdd;
            // 
            // lblCurreny
            // 
            this.lblCurreny.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCurreny.Appearance.Font")));
            resources.ApplyResources(this.lblCurreny, "lblCurreny");
            this.lblCurreny.Name = "lblCurreny";
            this.lblCurreny.StyleController = this.lcTransactionAdd;
            // 
            // lblActualAmount
            // 
            this.lblActualAmount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblActualAmount.Appearance.Font")));
            resources.ApplyResources(this.lblActualAmount, "lblActualAmount");
            this.lblActualAmount.Name = "lblActualAmount";
            this.lblActualAmount.StyleController = this.lcTransactionAdd;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcTransactionAdd;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtExchangeRate
            // 
            resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
            this.txtExchangeRate.Name = "txtExchangeRate";
            this.txtExchangeRate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtExchangeRate.StyleController = this.lcTransactionAdd;
            this.txtExchangeRate.TextChanged += new System.EventHandler(this.txtExchangeRate_TextChanged);
            // 
            // txtReceiptAmount
            // 
            resources.ApplyResources(this.txtReceiptAmount, "txtReceiptAmount");
            this.txtReceiptAmount.Name = "txtReceiptAmount";
            this.txtReceiptAmount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtReceiptAmount.Properties.DisplayFormat.FormatString = "C";
            this.txtReceiptAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtReceiptAmount.Properties.Mask.EditMask = resources.GetString("txtReceiptAmount.Properties.Mask.EditMask");
            this.txtReceiptAmount.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtReceiptAmount.Properties.Mask.MaskType")));
            this.txtReceiptAmount.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("txtReceiptAmount.Properties.Mask.UseMaskAsDisplayFormat")));
            this.txtReceiptAmount.StyleController = this.lcTransactionAdd;
            this.txtReceiptAmount.TextChanged += new System.EventHandler(this.txtReceiptAmount_TextChanged);
            // 
            // glkpPurpose
            // 
            resources.ApplyResources(this.glkpPurpose, "glkpPurpose");
            this.glkpPurpose.Name = "glkpPurpose";
            this.glkpPurpose.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpPurpose.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpPurpose.Properties.Buttons"))))});
            this.glkpPurpose.Properties.ImmediatePopup = true;
            this.glkpPurpose.Properties.NullText = resources.GetString("glkpPurpose.Properties.NullText");
            this.glkpPurpose.Properties.PopupFormSize = new System.Drawing.Size(351, 50);
            this.glkpPurpose.Properties.View = this.gridLookUpEdit3View;
            this.glkpPurpose.StyleController = this.lcTransactionAdd;
            // 
            // gridLookUpEdit3View
            // 
            this.gridLookUpEdit3View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPurposeID,
            this.colPurposeName});
            this.gridLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit3View.Name = "gridLookUpEdit3View";
            this.gridLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit3View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit3View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit3View.OptionsView.ShowIndicator = false;
            // 
            // colPurposeID
            // 
            resources.ApplyResources(this.colPurposeID, "colPurposeID");
            this.colPurposeID.FieldName = "CONTRIBUTION_HEAD_ID";
            this.colPurposeID.Name = "colPurposeID";
            // 
            // colPurposeName
            // 
            resources.ApplyResources(this.colPurposeName, "colPurposeName");
            this.colPurposeName.FieldName = "HEAD";
            this.colPurposeName.Name = "colPurposeName";
            // 
            // glkpReceiptType
            // 
            resources.ApplyResources(this.glkpReceiptType, "glkpReceiptType");
            this.glkpReceiptType.Name = "glkpReceiptType";
            this.glkpReceiptType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpReceiptType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpReceiptType.Properties.Buttons"))))});
            this.glkpReceiptType.Properties.NullText = resources.GetString("glkpReceiptType.Properties.NullText");
            this.glkpReceiptType.Properties.PopupFormMinSize = new System.Drawing.Size(108, 0);
            this.glkpReceiptType.Properties.PopupFormSize = new System.Drawing.Size(70, 20);
            this.glkpReceiptType.Properties.View = this.gridLookUpEdit2View;
            this.glkpReceiptType.StyleController = this.lcTransactionAdd;
            this.glkpReceiptType.EditValueChanged += new System.EventHandler(this.glkpReceiptType_EditValueChanged);
            // 
            // gridLookUpEdit2View
            // 
            this.gridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colReceiptID,
            this.colReceiptType});
            this.gridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit2View.Name = "gridLookUpEdit2View";
            this.gridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit2View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit2View.OptionsView.ShowIndicator = false;
            // 
            // colReceiptID
            // 
            resources.ApplyResources(this.colReceiptID, "colReceiptID");
            this.colReceiptID.FieldName = "Id";
            this.colReceiptID.Name = "colReceiptID";
            // 
            // colReceiptType
            // 
            resources.ApplyResources(this.colReceiptType, "colReceiptType");
            this.colReceiptType.FieldName = "ReceiptType";
            this.colReceiptType.Name = "colReceiptType";
            // 
            // glkpDonor
            // 
            resources.ApplyResources(this.glkpDonor, "glkpDonor");
            this.glkpDonor.Name = "glkpDonor";
            this.glkpDonor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpDonor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpDonor.Properties.Buttons"))))});
            this.glkpDonor.Properties.ImmediatePopup = true;
            this.glkpDonor.Properties.NullText = resources.GetString("glkpDonor.Properties.NullText");
            this.glkpDonor.Properties.PopupFormMinSize = new System.Drawing.Size(145, 0);
            this.glkpDonor.Properties.PopupFormSize = new System.Drawing.Size(100, 40);
            this.glkpDonor.Properties.View = this.gridView4;
            this.glkpDonor.StyleController = this.lcTransactionAdd;
            // 
            // gridView4
            // 
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDonorID,
            this.colDonorName});
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowColumnHeaders = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            this.gridView4.OptionsView.ShowIndicator = false;
            // 
            // colDonorID
            // 
            resources.ApplyResources(this.colDonorID, "colDonorID");
            this.colDonorID.FieldName = "DONAUD_ID";
            this.colDonorID.Name = "colDonorID";
            // 
            // colDonorName
            // 
            resources.ApplyResources(this.colDonorName, "colDonorName");
            this.colDonorName.FieldName = "NAME";
            this.colDonorName.Name = "colDonorName";
            // 
            // dteReconciledOn
            // 
            resources.ApplyResources(this.dteReconciledOn, "dteReconciledOn");
            this.dteReconciledOn.Name = "dteReconciledOn";
            this.dteReconciledOn.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dteReconciledOn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dteReconciledOn.Properties.Buttons"))))});
            this.dteReconciledOn.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteReconciledOn.StyleController = this.lcTransactionAdd;
            // 
            // txtChequeNo
            // 
            resources.ApplyResources(this.txtChequeNo, "txtChequeNo");
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtChequeNo.StyleController = this.lcTransactionAdd;
            // 
            // txtAmount
            // 
            resources.ApplyResources(this.txtAmount, "txtAmount");
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAmount.Properties.DisplayFormat.FormatString = "C";
            this.txtAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAmount.Properties.Mask.EditMask = resources.GetString("txtAmount.Properties.Mask.EditMask");
            this.txtAmount.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtAmount.Properties.Mask.MaskType")));
            this.txtAmount.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("txtAmount.Properties.Mask.UseMaskAsDisplayFormat")));
            this.txtAmount.StyleController = this.lcTransactionAdd;
            // 
            // txtVoucher
            // 
            resources.ApplyResources(this.txtVoucher, "txtVoucher");
            this.txtVoucher.Name = "txtVoucher";
            this.txtVoucher.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtVoucher.StyleController = this.lcTransactionAdd;
            // 
            // glkpCostCentre
            // 
            resources.ApplyResources(this.glkpCostCentre, "glkpCostCentre");
            this.glkpCostCentre.Name = "glkpCostCentre";
            this.glkpCostCentre.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpCostCentre.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCostCentre.Properties.Buttons"))))});
            this.glkpCostCentre.Properties.ImmediatePopup = true;
            this.glkpCostCentre.Properties.NullText = resources.GetString("glkpCostCentre.Properties.NullText");
            this.glkpCostCentre.Properties.PopupFormMinSize = new System.Drawing.Size(123, 0);
            this.glkpCostCentre.Properties.PopupFormSize = new System.Drawing.Size(110, 50);
            this.glkpCostCentre.Properties.View = this.gridView3;
            this.glkpCostCentre.StyleController = this.lcTransactionAdd;
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDCostCenterID,
            this.colAbbrevation,
            this.colCostCenterName});
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowColumnHeaders = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.OptionsView.ShowIndicator = false;
            // 
            // colIDCostCenterID
            // 
            resources.ApplyResources(this.colIDCostCenterID, "colIDCostCenterID");
            this.colIDCostCenterID.FieldName = "COST_CENTRE_ID";
            this.colIDCostCenterID.Name = "colIDCostCenterID";
            // 
            // colAbbrevation
            // 
            resources.ApplyResources(this.colAbbrevation, "colAbbrevation");
            this.colAbbrevation.FieldName = "ABBREVATION";
            this.colAbbrevation.Name = "colAbbrevation";
            // 
            // colCostCenterName
            // 
            resources.ApplyResources(this.colCostCenterName, "colCostCenterName");
            this.colCostCenterName.FieldName = "COST_CENTRE_NAME";
            this.colCostCenterName.Name = "colCostCenterName";
            // 
            // glkpCashBankLedger
            // 
            resources.ApplyResources(this.glkpCashBankLedger, "glkpCashBankLedger");
            this.glkpCashBankLedger.Name = "glkpCashBankLedger";
            this.glkpCashBankLedger.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpCashBankLedger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCashBankLedger.Properties.Buttons"))))});
            this.glkpCashBankLedger.Properties.ImmediatePopup = true;
            this.glkpCashBankLedger.Properties.NullText = resources.GetString("glkpCashBankLedger.Properties.NullText");
            this.glkpCashBankLedger.Properties.PopupFormSize = new System.Drawing.Size(351, 50);
            this.glkpCashBankLedger.Properties.View = this.gridView2;
            this.glkpCashBankLedger.StyleController = this.lcTransactionAdd;
            this.glkpCashBankLedger.EditValueChanged += new System.EventHandler(this.glkpCashBankLedger_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCashBankId,
            this.colCashBankName});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowColumnHeaders = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            // 
            // colCashBankId
            // 
            resources.ApplyResources(this.colCashBankId, "colCashBankId");
            this.colCashBankId.FieldName = "LEDGER_ID";
            this.colCashBankId.Name = "colCashBankId";
            // 
            // colCashBankName
            // 
            resources.ApplyResources(this.colCashBankName, "colCashBankName");
            this.colCashBankName.FieldName = "LEDGER_NAME";
            this.colCashBankName.Name = "colCashBankName";
            // 
            // glkpLedger
            // 
            resources.ApplyResources(this.glkpLedger, "glkpLedger");
            this.glkpLedger.Name = "glkpLedger";
            this.glkpLedger.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpLedger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpLedger.Properties.Buttons"))))});
            this.glkpLedger.Properties.ImmediatePopup = true;
            this.glkpLedger.Properties.NullText = resources.GetString("glkpLedger.Properties.NullText");
            this.glkpLedger.Properties.PopupFormSize = new System.Drawing.Size(351, 50);
            this.glkpLedger.Properties.View = this.gridView1;
            this.glkpLedger.StyleController = this.lcTransactionAdd;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerID,
            this.colLedgerCode,
            this.colLedgerName});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colLedgerID
            // 
            resources.ApplyResources(this.colLedgerID, "colLedgerID");
            this.colLedgerID.FieldName = "LEDGER_ID";
            this.colLedgerID.Name = "colLedgerID";
            // 
            // colLedgerCode
            // 
            this.colLedgerCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerCode.AppearanceHeader.Font")));
            this.colLedgerCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerCode, "colLedgerCode");
            this.colLedgerCode.FieldName = "LEDGER_CODE";
            this.colLedgerCode.Name = "colLedgerCode";
            // 
            // colLedgerName
            // 
            this.colLedgerName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerName.AppearanceHeader.Font")));
            this.colLedgerName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            // 
            // rboTransactionType
            // 
            resources.ApplyResources(this.rboTransactionType, "rboTransactionType");
            this.rboTransactionType.Name = "rboTransactionType";
            this.rboTransactionType.Properties.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("rboTransactionType.Properties.Appearance.BackColor")));
            this.rboTransactionType.Properties.Appearance.Options.UseBackColor = true;
            this.rboTransactionType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rboTransactionType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rboTransactionType.Properties.Items"))), resources.GetString("rboTransactionType.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rboTransactionType.Properties.Items2"))), resources.GetString("rboTransactionType.Properties.Items3")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rboTransactionType.Properties.Items4"))), resources.GetString("rboTransactionType.Properties.Items5"))});
            this.rboTransactionType.StyleController = this.lcTransactionAdd;
            this.rboTransactionType.SelectedIndexChanged += new System.EventHandler(this.rboTransactionType_SelectedIndexChanged);
            // 
            // glkpProject
            // 
            resources.ApplyResources(this.glkpProject, "glkpProject");
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons"))))});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = resources.GetString("glkpProject.Properties.NullText");
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(363, 50);
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.StyleController = this.lcTransactionAdd;
            this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colProjectName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProjectName
            // 
            resources.ApplyResources(this.colProjectName, "colProjectName");
            this.colProjectName.FieldName = "PROJECT";
            this.colProjectName.Name = "colProjectName";
            // 
            // dteTransactionDate
            // 
            resources.ApplyResources(this.dteTransactionDate, "dteTransactionDate");
            this.dteTransactionDate.Name = "dteTransactionDate";
            this.dteTransactionDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dteTransactionDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dteTransactionDate.Properties.Buttons"))))});
            this.dteTransactionDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTransactionDate.StyleController = this.lcTransactionAdd;
            // 
            // lcgTransaction
            // 
            resources.ApplyResources(this.lcgTransaction, "lcgTransaction");
            this.lcgTransaction.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgTransaction.GroupBordersVisible = false;
            this.lcgTransaction.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblProject,
            this.lblTransactionType,
            this.lcgTransactionGroup,
            this.lblSave,
            this.lblClose,
            this.emptySpaceItem1,
            this.lblTransactionDate,
            this.lblVoucher,
            this.emptySpaceItem5});
            this.lcgTransaction.Location = new System.Drawing.Point(0, 0);
            this.lcgTransaction.Name = "Root";
            this.lcgTransaction.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgTransaction.Size = new System.Drawing.Size(463, 350);
            this.lcgTransaction.TextVisible = false;
            // 
            // lblProject
            // 
            this.lblProject.AllowHtmlStringInCaption = true;
            this.lblProject.Control = this.glkpProject;
            resources.ApplyResources(this.lblProject, "lblProject");
            this.lblProject.Location = new System.Drawing.Point(0, 0);
            this.lblProject.Name = "lblProject";
            this.lblProject.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 5, 3, 3);
            this.lblProject.Size = new System.Drawing.Size(463, 26);
            this.lblProject.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblTransactionType
            // 
            this.lblTransactionType.AllowHtmlStringInCaption = true;
            this.lblTransactionType.Control = this.rboTransactionType;
            resources.ApplyResources(this.lblTransactionType, "lblTransactionType");
            this.lblTransactionType.Location = new System.Drawing.Point(0, 26);
            this.lblTransactionType.MaxSize = new System.Drawing.Size(0, 28);
            this.lblTransactionType.MinSize = new System.Drawing.Size(154, 28);
            this.lblTransactionType.Name = "lblTransactionType";
            this.lblTransactionType.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 5, 0, 3);
            this.lblTransactionType.Size = new System.Drawing.Size(463, 28);
            this.lblTransactionType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblTransactionType.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lcgTransactionGroup
            // 
            this.lcgTransactionGroup.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgTransactionGroup.AppearanceGroup.Font")));
            this.lcgTransactionGroup.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgTransactionGroup, "lcgTransactionGroup");
            this.lcgTransactionGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblLedger,
            this.lblCashBank,
            this.lblCostCenter,
            this.lblAmount,
            this.lblDonor,
            this.lblPurpose,
            this.lblReceiptType,
            this.lblReceiptAmount,
            this.lblExchangeRate,
            this.esiExchangeRate,
            this.lblReceiptActualAmount,
            this.lblCurrencySymbol,
            this.esiCurrenySymbol,
            this.esiActualAmount,
            this.lblNarration,
            this.lblChequeNo,
            this.lblReconciledOn,
            this.emptySpaceItem3,
            this.emptySpaceItem2});
            this.lcgTransactionGroup.Location = new System.Drawing.Point(0, 80);
            this.lcgTransactionGroup.Name = "lcgTransactionGroup";
            this.lcgTransactionGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.lcgTransactionGroup.Size = new System.Drawing.Size(463, 242);
            this.lcgTransactionGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 5, 0);
            // 
            // lblLedger
            // 
            this.lblLedger.AllowHtmlStringInCaption = true;
            this.lblLedger.Control = this.glkpLedger;
            resources.ApplyResources(this.lblLedger, "lblLedger");
            this.lblLedger.Location = new System.Drawing.Point(0, 92);
            this.lblLedger.MaxSize = new System.Drawing.Size(455, 26);
            this.lblLedger.MinSize = new System.Drawing.Size(455, 26);
            this.lblLedger.Name = "lblLedger";
            this.lblLedger.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblLedger.Size = new System.Drawing.Size(455, 26);
            this.lblLedger.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblLedger.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblCashBank
            // 
            this.lblCashBank.AllowHtmlStringInCaption = true;
            this.lblCashBank.Control = this.glkpCashBankLedger;
            resources.ApplyResources(this.lblCashBank, "lblCashBank");
            this.lblCashBank.Location = new System.Drawing.Point(0, 118);
            this.lblCashBank.MaxSize = new System.Drawing.Size(452, 23);
            this.lblCashBank.MinSize = new System.Drawing.Size(452, 23);
            this.lblCashBank.Name = "lblCashBank";
            this.lblCashBank.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblCashBank.Size = new System.Drawing.Size(455, 23);
            this.lblCashBank.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCashBank.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblCostCenter
            // 
            this.lblCostCenter.Control = this.glkpCostCentre;
            resources.ApplyResources(this.lblCostCenter, "lblCostCenter");
            this.lblCostCenter.Location = new System.Drawing.Point(0, 141);
            this.lblCostCenter.MaxSize = new System.Drawing.Size(232, 23);
            this.lblCostCenter.MinSize = new System.Drawing.Size(232, 23);
            this.lblCostCenter.Name = "lblCostCenter";
            this.lblCostCenter.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblCostCenter.Size = new System.Drawing.Size(232, 23);
            this.lblCostCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCostCenter.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblAmount
            // 
            this.lblAmount.AllowHtmlStringInCaption = true;
            this.lblAmount.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblAmount.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblAmount.Control = this.txtAmount;
            resources.ApplyResources(this.lblAmount, "lblAmount");
            this.lblAmount.Location = new System.Drawing.Point(245, 141);
            this.lblAmount.MaxSize = new System.Drawing.Size(207, 23);
            this.lblAmount.MinSize = new System.Drawing.Size(207, 23);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblAmount.Size = new System.Drawing.Size(210, 23);
            this.lblAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAmount.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblDonor
            // 
            this.lblDonor.Control = this.glkpDonor;
            resources.ApplyResources(this.lblDonor, "lblDonor");
            this.lblDonor.Location = new System.Drawing.Point(0, 0);
            this.lblDonor.MaxSize = new System.Drawing.Size(245, 26);
            this.lblDonor.MinSize = new System.Drawing.Size(245, 26);
            this.lblDonor.Name = "lblDonor";
            this.lblDonor.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblDonor.Size = new System.Drawing.Size(245, 26);
            this.lblDonor.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDonor.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblPurpose
            // 
            this.lblPurpose.Control = this.glkpPurpose;
            resources.ApplyResources(this.lblPurpose, "lblPurpose");
            this.lblPurpose.Location = new System.Drawing.Point(0, 26);
            this.lblPurpose.MaxSize = new System.Drawing.Size(452, 23);
            this.lblPurpose.MinSize = new System.Drawing.Size(452, 23);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblPurpose.Size = new System.Drawing.Size(455, 23);
            this.lblPurpose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblPurpose.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblReceiptType
            // 
            this.lblReceiptType.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblReceiptType.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblReceiptType.Control = this.glkpReceiptType;
            resources.ApplyResources(this.lblReceiptType, "lblReceiptType");
            this.lblReceiptType.Location = new System.Drawing.Point(245, 0);
            this.lblReceiptType.MaxSize = new System.Drawing.Size(207, 26);
            this.lblReceiptType.MinSize = new System.Drawing.Size(207, 26);
            this.lblReceiptType.Name = "lblReceiptType";
            this.lblReceiptType.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblReceiptType.Size = new System.Drawing.Size(210, 26);
            this.lblReceiptType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblReceiptType.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblReceiptAmount
            // 
            this.lblReceiptAmount.Control = this.txtReceiptAmount;
            resources.ApplyResources(this.lblReceiptAmount, "lblReceiptAmount");
            this.lblReceiptAmount.Location = new System.Drawing.Point(0, 49);
            this.lblReceiptAmount.MaxSize = new System.Drawing.Size(216, 23);
            this.lblReceiptAmount.MinSize = new System.Drawing.Size(216, 23);
            this.lblReceiptAmount.Name = "lblReceiptAmount";
            this.lblReceiptAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblReceiptAmount.Size = new System.Drawing.Size(216, 23);
            this.lblReceiptAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblReceiptAmount.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblExchangeRate
            // 
            this.lblExchangeRate.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblExchangeRate.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblExchangeRate.Control = this.txtExchangeRate;
            resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
            this.lblExchangeRate.Location = new System.Drawing.Point(245, 49);
            this.lblExchangeRate.MaxSize = new System.Drawing.Size(207, 23);
            this.lblExchangeRate.MinSize = new System.Drawing.Size(207, 23);
            this.lblExchangeRate.Name = "lblExchangeRate";
            this.lblExchangeRate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblExchangeRate.Size = new System.Drawing.Size(210, 23);
            this.lblExchangeRate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblExchangeRate.TextSize = new System.Drawing.Size(95, 13);
            // 
            // esiExchangeRate
            // 
            this.esiExchangeRate.AllowHotTrack = false;
            resources.ApplyResources(this.esiExchangeRate, "esiExchangeRate");
            this.esiExchangeRate.Location = new System.Drawing.Point(216, 49);
            this.esiExchangeRate.MaxSize = new System.Drawing.Size(29, 23);
            this.esiExchangeRate.MinSize = new System.Drawing.Size(29, 23);
            this.esiExchangeRate.Name = "esiExchangeRate";
            this.esiExchangeRate.Size = new System.Drawing.Size(29, 23);
            this.esiExchangeRate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.esiExchangeRate.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblReceiptActualAmount
            // 
            this.lblReceiptActualAmount.Control = this.lblActualAmount;
            resources.ApplyResources(this.lblReceiptActualAmount, "lblReceiptActualAmount");
            this.lblReceiptActualAmount.Location = new System.Drawing.Point(100, 72);
            this.lblReceiptActualAmount.MaxSize = new System.Drawing.Size(117, 20);
            this.lblReceiptActualAmount.MinSize = new System.Drawing.Size(117, 20);
            this.lblReceiptActualAmount.Name = "lblReceiptActualAmount";
            this.lblReceiptActualAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblReceiptActualAmount.Size = new System.Drawing.Size(117, 20);
            this.lblReceiptActualAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblReceiptActualAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblReceiptActualAmount.TextSize = new System.Drawing.Size(70, 13);
            this.lblReceiptActualAmount.TextToControlDistance = 5;
            // 
            // lblCurrencySymbol
            // 
            this.lblCurrencySymbol.Control = this.lblCurreny;
            resources.ApplyResources(this.lblCurrencySymbol, "lblCurrencySymbol");
            this.lblCurrencySymbol.Location = new System.Drawing.Point(345, 72);
            this.lblCurrencySymbol.MaxSize = new System.Drawing.Size(110, 20);
            this.lblCurrencySymbol.MinSize = new System.Drawing.Size(110, 20);
            this.lblCurrencySymbol.Name = "lblCurrencySymbol";
            this.lblCurrencySymbol.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblCurrencySymbol.Size = new System.Drawing.Size(110, 20);
            this.lblCurrencySymbol.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCurrencySymbol.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCurrencySymbol.TextSize = new System.Drawing.Size(81, 13);
            this.lblCurrencySymbol.TextToControlDistance = 5;
            // 
            // esiCurrenySymbol
            // 
            this.esiCurrenySymbol.AllowHotTrack = false;
            resources.ApplyResources(this.esiCurrenySymbol, "esiCurrenySymbol");
            this.esiCurrenySymbol.Location = new System.Drawing.Point(217, 72);
            this.esiCurrenySymbol.MaxSize = new System.Drawing.Size(128, 20);
            this.esiCurrenySymbol.MinSize = new System.Drawing.Size(128, 20);
            this.esiCurrenySymbol.Name = "esiCurrenySymbol";
            this.esiCurrenySymbol.Size = new System.Drawing.Size(128, 20);
            this.esiCurrenySymbol.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.esiCurrenySymbol.TextSize = new System.Drawing.Size(0, 0);
            // 
            // esiActualAmount
            // 
            this.esiActualAmount.AllowHotTrack = false;
            resources.ApplyResources(this.esiActualAmount, "esiActualAmount");
            this.esiActualAmount.Location = new System.Drawing.Point(0, 72);
            this.esiActualAmount.MaxSize = new System.Drawing.Size(100, 20);
            this.esiActualAmount.MinSize = new System.Drawing.Size(100, 20);
            this.esiActualAmount.Name = "esiActualAmount";
            this.esiActualAmount.Size = new System.Drawing.Size(100, 20);
            this.esiActualAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.esiActualAmount.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblNarration
            // 
            this.lblNarration.Control = this.txtNarration;
            resources.ApplyResources(this.lblNarration, "lblNarration");
            this.lblNarration.Location = new System.Drawing.Point(0, 187);
            this.lblNarration.MaxSize = new System.Drawing.Size(452, 23);
            this.lblNarration.MinSize = new System.Drawing.Size(452, 23);
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblNarration.Size = new System.Drawing.Size(455, 23);
            this.lblNarration.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblNarration.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblChequeNo
            // 
            this.lblChequeNo.Control = this.txtChequeNo;
            resources.ApplyResources(this.lblChequeNo, "lblChequeNo");
            this.lblChequeNo.Location = new System.Drawing.Point(0, 164);
            this.lblChequeNo.MaxSize = new System.Drawing.Size(232, 23);
            this.lblChequeNo.MinSize = new System.Drawing.Size(232, 23);
            this.lblChequeNo.Name = "lblChequeNo";
            this.lblChequeNo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblChequeNo.Size = new System.Drawing.Size(232, 23);
            this.lblChequeNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblChequeNo.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblReconciledOn
            // 
            this.lblReconciledOn.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblReconciledOn.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblReconciledOn.Control = this.dteReconciledOn;
            resources.ApplyResources(this.lblReconciledOn, "lblReconciledOn");
            this.lblReconciledOn.Location = new System.Drawing.Point(245, 164);
            this.lblReconciledOn.MaxSize = new System.Drawing.Size(207, 23);
            this.lblReconciledOn.MinSize = new System.Drawing.Size(207, 23);
            this.lblReconciledOn.Name = "lblReconciledOn";
            this.lblReconciledOn.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblReconciledOn.Size = new System.Drawing.Size(210, 23);
            this.lblReconciledOn.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblReconciledOn.TextSize = new System.Drawing.Size(95, 13);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(232, 164);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(13, 23);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(232, 141);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(13, 23);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(13, 23);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(13, 23);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblSave
            // 
            this.lblSave.Control = this.btnSave;
            resources.ApplyResources(this.lblSave, "lblSave");
            this.lblSave.Location = new System.Drawing.Point(327, 322);
            this.lblSave.MaxSize = new System.Drawing.Size(66, 28);
            this.lblSave.MinSize = new System.Drawing.Size(66, 28);
            this.lblSave.Name = "lblSave";
            this.lblSave.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblSave.Size = new System.Drawing.Size(66, 28);
            this.lblSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblSave.TextSize = new System.Drawing.Size(0, 0);
            this.lblSave.TextToControlDistance = 0;
            this.lblSave.TextVisible = false;
            // 
            // lblClose
            // 
            this.lblClose.Control = this.btnClose;
            resources.ApplyResources(this.lblClose, "lblClose");
            this.lblClose.Location = new System.Drawing.Point(393, 322);
            this.lblClose.MaxSize = new System.Drawing.Size(70, 28);
            this.lblClose.MinSize = new System.Drawing.Size(70, 28);
            this.lblClose.Name = "lblClose";
            this.lblClose.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblClose.Size = new System.Drawing.Size(70, 28);
            this.lblClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblClose.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblClose.TextSize = new System.Drawing.Size(0, 0);
            this.lblClose.TextToControlDistance = 0;
            this.lblClose.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 322);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem1.Size = new System.Drawing.Size(327, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblTransactionDate
            // 
            this.lblTransactionDate.Control = this.dteTransactionDate;
            resources.ApplyResources(this.lblTransactionDate, "lblTransactionDate");
            this.lblTransactionDate.Location = new System.Drawing.Point(0, 54);
            this.lblTransactionDate.MaxSize = new System.Drawing.Size(181, 26);
            this.lblTransactionDate.MinSize = new System.Drawing.Size(181, 26);
            this.lblTransactionDate.Name = "lblTransactionDate";
            this.lblTransactionDate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblTransactionDate.Size = new System.Drawing.Size(181, 26);
            this.lblTransactionDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblTransactionDate.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblVoucher
            // 
            this.lblVoucher.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblVoucher.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblVoucher.Control = this.txtVoucher;
            resources.ApplyResources(this.lblVoucher, "lblVoucher");
            this.lblVoucher.Location = new System.Drawing.Point(247, 54);
            this.lblVoucher.MaxSize = new System.Drawing.Size(216, 26);
            this.lblVoucher.MinSize = new System.Drawing.Size(216, 26);
            this.lblVoucher.Name = "lblVoucher";
            this.lblVoucher.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 5, 3, 3);
            this.lblVoucher.Size = new System.Drawing.Size(216, 26);
            this.lblVoucher.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblVoucher.TextSize = new System.Drawing.Size(95, 13);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem5, "emptySpaceItem5");
            this.emptySpaceItem5.Location = new System.Drawing.Point(181, 54);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(66, 26);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(66, 26);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(66, 26);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmTransaction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lcTransactionAdd);
            this.Name = "frmTransaction";
            this.Load += new System.EventHandler(this.frmTransaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcTransactionAdd)).EndInit();
            this.lcTransactionAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPurpose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit3View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpReceiptType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpDonor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReconciledOn.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReconciledOn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChequeNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCostCentre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCashBankLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rboTransactionType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTransactionDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTransactionDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTransactionGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCashBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCostCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPurpose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReceiptType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReceiptAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExchangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiExchangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReceiptActualAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrencySymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiCurrenySymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiActualAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblChequeNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReconciledOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcTransactionAdd;
        private DevExpress.XtraLayout.LayoutControlGroup lcgTransaction;
        private DevExpress.XtraEditors.RadioGroup rboTransactionType;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem lblProject;
        private DevExpress.XtraLayout.LayoutControlItem lblTransactionType;
        private DevExpress.XtraLayout.LayoutControlGroup lcgTransactionGroup;
        private DevExpress.XtraEditors.GridLookUpEdit glkpPurpose;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit3View;
        private DevExpress.XtraEditors.GridLookUpEdit glkpReceiptType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraEditors.GridLookUpEdit glkpDonor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.DateEdit dteReconciledOn;
        private DevExpress.XtraEditors.TextEdit txtChequeNo;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        private DevExpress.XtraEditors.TextEdit txtVoucher;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCostCentre;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCashBankLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.GridLookUpEdit glkpLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.DateEdit dteTransactionDate;
        private DevExpress.XtraLayout.LayoutControlItem lblLedger;
        private DevExpress.XtraLayout.LayoutControlItem lblCashBank;
        private DevExpress.XtraLayout.LayoutControlItem lblCostCenter;
        private DevExpress.XtraLayout.LayoutControlItem lblTransactionDate;
        private DevExpress.XtraLayout.LayoutControlItem lblVoucher;
        private DevExpress.XtraLayout.LayoutControlItem lblAmount;
        private DevExpress.XtraLayout.LayoutControlItem lblChequeNo;
        private DevExpress.XtraLayout.LayoutControlItem lblReconciledOn;
        private DevExpress.XtraLayout.LayoutControlItem lblDonor;
        private DevExpress.XtraLayout.LayoutControlItem lblPurpose;
        private DevExpress.XtraLayout.LayoutControlItem lblReceiptType;
        private DevExpress.XtraEditors.TextEdit txtExchangeRate;
        private DevExpress.XtraEditors.TextEdit txtReceiptAmount;
        private DevExpress.XtraLayout.LayoutControlItem lblReceiptAmount;
        private DevExpress.XtraLayout.LayoutControlItem lblExchangeRate;
        private DevExpress.XtraEditors.LabelControl lblCurreny;
        private DevExpress.XtraEditors.LabelControl lblActualAmount;
        private DevExpress.XtraLayout.EmptySpaceItem esiExchangeRate;
        private DevExpress.XtraLayout.LayoutControlItem lblReceiptActualAmount;
        private DevExpress.XtraLayout.LayoutControlItem lblCurrencySymbol;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtNarration;
        private DevExpress.XtraLayout.EmptySpaceItem esiCurrenySymbol;
        private DevExpress.XtraLayout.EmptySpaceItem esiActualAmount;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlItem lblNarration;
        private DevExpress.XtraLayout.LayoutControlItem lblSave;
        private DevExpress.XtraLayout.LayoutControlItem lblClose;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiptID;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiptType;
        private DevExpress.XtraGrid.Columns.GridColumn colDonorID;
        private DevExpress.XtraGrid.Columns.GridColumn colDonorName;
        private DevExpress.XtraGrid.Columns.GridColumn colPurposeID;
        private DevExpress.XtraGrid.Columns.GridColumn colPurposeName;
        private DevExpress.XtraGrid.Columns.GridColumn colCashBankId;
        private DevExpress.XtraGrid.Columns.GridColumn colCashBankName;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerID;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colIDCostCenterID;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCenterName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colAbbrevation;
    }
}