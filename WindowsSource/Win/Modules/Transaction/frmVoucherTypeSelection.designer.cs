namespace ACPP.Modules.Transaction
{
    partial class frmVoucherTypeSelection
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
            this.lygDatePicker = new DevExpress.XtraLayout.LayoutControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.glkpVoucherType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVOUCHERTYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lygVoucherType = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcVouhcerType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.lygDatePicker)).BeginInit();
            this.lygDatePicker.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpVoucherType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lygVoucherType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcVouhcerType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // lygDatePicker
            // 
            this.lygDatePicker.Controls.Add(this.btnOk);
            this.lygDatePicker.Controls.Add(this.btnCancel);
            this.lygDatePicker.Controls.Add(this.glkpVoucherType);
            this.lygDatePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lygDatePicker.Location = new System.Drawing.Point(4, 4);
            this.lygDatePicker.Name = "lygDatePicker";
            this.lygDatePicker.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(471, 327, 250, 350);
            this.lygDatePicker.Root = this.layoutControlGroup1;
            this.lygDatePicker.Size = new System.Drawing.Size(287, 79);
            this.lygDatePicker.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(139, 52);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(71, 24);
            this.btnOk.StyleController = this.lygDatePicker;
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(214, 52);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.StyleController = this.lygDatePicker;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // glkpVoucherType
            // 
            this.glkpVoucherType.EnterMoveNextControl = true;
            this.glkpVoucherType.Location = new System.Drawing.Point(3, 22);
            this.glkpVoucherType.Name = "glkpVoucherType";
            this.glkpVoucherType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glkpVoucherType.Properties.Appearance.Options.UseFont = true;
            this.glkpVoucherType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpVoucherType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpVoucherType.Properties.ImmediatePopup = true;
            this.glkpVoucherType.Properties.NullText = "";
            this.glkpVoucherType.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpVoucherType.Properties.PopupFormSize = new System.Drawing.Size(279, 50);
            this.glkpVoucherType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpVoucherType.Properties.View = this.gridLookUpEdit2View;
            this.glkpVoucherType.Size = new System.Drawing.Size(281, 26);
            this.glkpVoucherType.StyleController = this.lygDatePicker;
            this.glkpVoucherType.TabIndex = 3;
            this.glkpVoucherType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glkpVoucherType_KeyDown);
            // 
            // gridLookUpEdit2View
            // 
            this.gridLookUpEdit2View.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVoucherId,
            this.colVOUCHERTYPE});
            this.gridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit2View.Name = "gridLookUpEdit2View";
            this.gridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit2View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit2View.OptionsView.ShowIndicator = false;
            // 
            // colVoucherId
            // 
            this.colVoucherId.Caption = "VoucherId";
            this.colVoucherId.FieldName = "VOUCHER_ID";
            this.colVoucherId.Name = "colVoucherId";
            // 
            // colVOUCHERTYPE
            // 
            this.colVOUCHERTYPE.Caption = "Voucher Type";
            this.colVOUCHERTYPE.FieldName = "VOUCHER_NAME";
            this.colVOUCHERTYPE.Name = "colVOUCHERTYPE";
            this.colVOUCHERTYPE.Visible = true;
            this.colVOUCHERTYPE.VisibleIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lygVoucherType});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(287, 79);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lygVoucherType
            // 
            this.lygVoucherType.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lygVoucherType.AppearanceGroup.Options.UseFont = true;
            this.lygVoucherType.CaptionImagePadding = new DevExpress.XtraLayout.Utils.Padding(1, 2, 1, 1);
            this.lygVoucherType.ContentImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lygVoucherType.CustomizationFormText = "Voucher Type";
            this.lygVoucherType.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcVouhcerType,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.lygVoucherType.Location = new System.Drawing.Point(0, 0);
            this.lygVoucherType.Name = "lygVoucherType";
            this.lygVoucherType.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lygVoucherType.Size = new System.Drawing.Size(287, 79);
            this.lygVoucherType.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lygVoucherType.Text = "Voucher Type";
            // 
            // lcVouhcerType
            // 
            this.lcVouhcerType.Control = this.glkpVoucherType;
            this.lcVouhcerType.CustomizationFormText = "lcVouhcerType";
            this.lcVouhcerType.Location = new System.Drawing.Point(0, 0);
            this.lcVouhcerType.Name = "lcVouhcerType";
            this.lcVouhcerType.Size = new System.Drawing.Size(285, 30);
            this.lcVouhcerType.Text = "lcVouhcerType";
            this.lcVouhcerType.TextSize = new System.Drawing.Size(0, 0);
            this.lcVouhcerType.TextToControlDistance = 0;
            this.lcVouhcerType.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnCancel;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(211, 30);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(74, 28);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(74, 28);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(74, 28);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOk;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(136, 30);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(75, 28);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(75, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(75, 28);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 30);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(136, 28);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmVoucherTypeSelection
            // 
            this.AcceptButton = this.btnOk;
            this.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.Appearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.Appearance.Options.UseBorderColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(296, 88);
            this.Controls.Add(this.lygDatePicker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmVoucherTypeSelection";
            this.Padding = new System.Windows.Forms.Padding(4, 4, 5, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Date";
            this.Load += new System.EventHandler(this.frmVoucherTypeSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lygDatePicker)).EndInit();
            this.lygDatePicker.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpVoucherType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lygVoucherType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcVouhcerType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lygDatePicker;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lygVoucherType;
        private DevExpress.XtraEditors.GridLookUpEdit glkpVoucherType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colVOUCHERTYPE;
        private DevExpress.XtraLayout.LayoutControlItem lcVouhcerType;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}