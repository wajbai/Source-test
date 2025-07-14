namespace ACPP.Modules.Transaction
{
    partial class frmVoucherBankReferenceDetails
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
            this.lcCtlBankReferenceDetails = new DevExpress.XtraLayout.LayoutControl();
            this.glkpFundTransfer = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFundTransferId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFundTransferName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtBranch = new DevExpress.XtraEditors.TextEdit();
            this.txtBankName = new DevExpress.XtraEditors.TextEdit();
            this.dateDate = new DevExpress.XtraEditors.DateEdit();
            this.lcgrpBankReferencedetails = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcGrpBankReferenceNo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcBankName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemBranch = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciFundTransfer = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcCtlBankReferenceDetails)).BeginInit();
            this.lcCtlBankReferenceDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpFundTransfer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgrpBankReferencedetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpBankReferenceNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBankName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciFundTransfer)).BeginInit();
            this.SuspendLayout();
            // 
            // lcCtlBankReferenceDetails
            // 
            this.lcCtlBankReferenceDetails.Controls.Add(this.glkpFundTransfer);
            this.lcCtlBankReferenceDetails.Controls.Add(this.btnOk);
            this.lcCtlBankReferenceDetails.Controls.Add(this.btnCancel);
            this.lcCtlBankReferenceDetails.Controls.Add(this.txtBranch);
            this.lcCtlBankReferenceDetails.Controls.Add(this.txtBankName);
            this.lcCtlBankReferenceDetails.Controls.Add(this.dateDate);
            this.lcCtlBankReferenceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcCtlBankReferenceDetails.Location = new System.Drawing.Point(5, 5);
            this.lcCtlBankReferenceDetails.Margin = new System.Windows.Forms.Padding(1);
            this.lcCtlBankReferenceDetails.Name = "lcCtlBankReferenceDetails";
            this.lcCtlBankReferenceDetails.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(608, 216, 328, 395);
            this.lcCtlBankReferenceDetails.Root = this.lcgrpBankReferencedetails;
            this.lcCtlBankReferenceDetails.Size = new System.Drawing.Size(336, 130);
            this.lcCtlBankReferenceDetails.TabIndex = 0;
            this.lcCtlBankReferenceDetails.Text = "layoutControl1";
            // 
            // glkpFundTransfer
            // 
            this.glkpFundTransfer.Location = new System.Drawing.Point(236, 27);
            this.glkpFundTransfer.Name = "glkpFundTransfer";
            this.glkpFundTransfer.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpFundTransfer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpFundTransfer.Properties.NullText = "";
            this.glkpFundTransfer.Properties.PopupFormSize = new System.Drawing.Size(20, 20);
            this.glkpFundTransfer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpFundTransfer.Properties.View = this.gridLookUpEdit1View;
            this.glkpFundTransfer.Size = new System.Drawing.Size(92, 20);
            this.glkpFundTransfer.StyleController = this.lcCtlBankReferenceDetails;
            this.glkpFundTransfer.TabIndex = 9;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFundTransferId,
            this.colFundTransferName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colFundTransferId
            // 
            this.colFundTransferId.Caption = "Fund TransferId";
            this.colFundTransferId.FieldName = "FUND_TRANSFER_TYPE_ID";
            this.colFundTransferId.Name = "colFundTransferId";
            // 
            // colFundTransferName
            // 
            this.colFundTransferName.Caption = "Fund Transfer";
            this.colFundTransferName.FieldName = "FUND_TRANSFER_TYPE_NAME";
            this.colFundTransferName.Name = "colFundTransferName";
            this.colFundTransferName.Visible = true;
            this.colFundTransferName.VisibleIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(224, 99);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(45, 22);
            this.btnOk.StyleController = this.lcCtlBankReferenceDetails;
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "&Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(273, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 22);
            this.btnCancel.StyleController = this.lcCtlBankReferenceDetails;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtBranch
            // 
            this.txtBranch.Location = new System.Drawing.Point(67, 75);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtBranch.Properties.MaxLength = 50;
            this.txtBranch.Size = new System.Drawing.Size(261, 20);
            this.txtBranch.StyleController = this.lcCtlBankReferenceDetails;
            this.txtBranch.TabIndex = 6;
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(67, 51);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtBankName.Properties.MaxLength = 50;
            this.txtBankName.Size = new System.Drawing.Size(261, 20);
            this.txtBankName.StyleController = this.lcCtlBankReferenceDetails;
            this.txtBankName.TabIndex = 5;
            // 
            // dateDate
            // 
            this.dateDate.EditValue = null;
            this.dateDate.Location = new System.Drawing.Point(67, 27);
            this.dateDate.Name = "dateDate";
            this.dateDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateDate.Size = new System.Drawing.Size(90, 20);
            this.dateDate.StyleController = this.lcCtlBankReferenceDetails;
            this.dateDate.TabIndex = 4;
            // 
            // lcgrpBankReferencedetails
            // 
            this.lcgrpBankReferencedetails.CustomizationFormText = "2323232 : Bank Reference Details";
            this.lcgrpBankReferencedetails.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgrpBankReferencedetails.GroupBordersVisible = false;
            this.lcgrpBankReferencedetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcGrpBankReferenceNo});
            this.lcgrpBankReferencedetails.Location = new System.Drawing.Point(0, 0);
            this.lcgrpBankReferencedetails.Name = "Root";
            this.lcgrpBankReferencedetails.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgrpBankReferencedetails.Size = new System.Drawing.Size(336, 130);
            this.lcgrpBankReferencedetails.Text = "2323232 : Bank Reference Details";
            // 
            // lcGrpBankReferenceNo
            // 
            this.lcGrpBankReferenceNo.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lcGrpBankReferenceNo.AppearanceGroup.Options.UseFont = true;
            this.lcGrpBankReferenceNo.CustomizationFormText = "Bank Reference Number (DD/Cheque) details";
            this.lcGrpBankReferenceNo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcDate,
            this.lcBankName,
            this.lcItemBranch,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.lciFundTransfer});
            this.lcGrpBankReferenceNo.Location = new System.Drawing.Point(0, 0);
            this.lcGrpBankReferenceNo.Name = "lcGrpBankReferenceNo";
            this.lcGrpBankReferenceNo.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcGrpBankReferenceNo.Size = new System.Drawing.Size(334, 128);
            this.lcGrpBankReferenceNo.Text = "Bank Reference Number (DD/Cheque) details";
            // 
            // lcDate
            // 
            this.lcDate.Control = this.dateDate;
            this.lcDate.CustomizationFormText = "Date";
            this.lcDate.Location = new System.Drawing.Point(0, 0);
            this.lcDate.Name = "lcDate";
            this.lcDate.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcDate.Size = new System.Drawing.Size(153, 24);
            this.lcDate.Text = "Date";
            this.lcDate.TextSize = new System.Drawing.Size(53, 13);
            // 
            // lcBankName
            // 
            this.lcBankName.Control = this.txtBankName;
            this.lcBankName.CustomizationFormText = "Bank Name";
            this.lcBankName.Location = new System.Drawing.Point(0, 24);
            this.lcBankName.Name = "lcBankName";
            this.lcBankName.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcBankName.Size = new System.Drawing.Size(324, 24);
            this.lcBankName.Text = "Bank Name";
            this.lcBankName.TextSize = new System.Drawing.Size(53, 13);
            // 
            // lcItemBranch
            // 
            this.lcItemBranch.Control = this.txtBranch;
            this.lcItemBranch.CustomizationFormText = "Branch";
            this.lcItemBranch.Location = new System.Drawing.Point(0, 48);
            this.lcItemBranch.Name = "lcItemBranch";
            this.lcItemBranch.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcItemBranch.Size = new System.Drawing.Size(324, 24);
            this.lcItemBranch.Text = "Branch";
            this.lcItemBranch.TextSize = new System.Drawing.Size(53, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 72);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(216, 27);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnOk;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(216, 72);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(49, 27);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(265, 72);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(59, 27);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // lciFundTransfer
            // 
            this.lciFundTransfer.Control = this.glkpFundTransfer;
            this.lciFundTransfer.CustomizationFormText = "Fund Transfer";
            this.lciFundTransfer.Location = new System.Drawing.Point(153, 0);
            this.lciFundTransfer.Name = "lciFundTransfer";
            this.lciFundTransfer.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 0);
            this.lciFundTransfer.Size = new System.Drawing.Size(171, 24);
            this.lciFundTransfer.Text = "Transfer Mode";
            this.lciFundTransfer.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lciFundTransfer.TextSize = new System.Drawing.Size(70, 13);
            this.lciFundTransfer.TextToControlDistance = 5;
            // 
            // frmVoucherBankReferenceDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(346, 140);
            this.Controls.Add(this.lcCtlBankReferenceDetails);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVoucherBankReferenceDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bank Reference Number (DD/Cheque) details";
            this.Load += new System.EventHandler(this.frmVoucherBankReferenceDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcCtlBankReferenceDetails)).EndInit();
            this.lcCtlBankReferenceDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpFundTransfer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgrpBankReferencedetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpBankReferenceNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBankName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciFundTransfer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcCtlBankReferenceDetails;
        private DevExpress.XtraLayout.LayoutControlGroup lcgrpBankReferencedetails;
        private DevExpress.XtraEditors.TextEdit txtBranch;
        private DevExpress.XtraEditors.TextEdit txtBankName;
        private DevExpress.XtraEditors.DateEdit dateDate;
        private DevExpress.XtraLayout.LayoutControlItem lcDate;
        private DevExpress.XtraLayout.LayoutControlItem lcBankName;
        private DevExpress.XtraLayout.LayoutControlItem lcItemBranch;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup lcGrpBankReferenceNo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.GridLookUpEdit glkpFundTransfer;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colFundTransferId;
        private DevExpress.XtraGrid.Columns.GridColumn colFundTransferName;
        private DevExpress.XtraLayout.LayoutControlItem lciFundTransfer;


    }
}