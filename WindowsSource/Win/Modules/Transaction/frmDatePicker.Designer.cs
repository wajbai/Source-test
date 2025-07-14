namespace ACPP.Modules.Transaction
{
    partial class frmDatePicker
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
            this.dtDateTo = new DevExpress.XtraEditors.DateEdit();
            this.dtDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.dteVoucherDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lygVoucherDate = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lygChangePeriod = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lyDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.lyDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lygDatePicker)).BeginInit();
            this.lygDatePicker.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteVoucherDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteVoucherDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lygVoucherDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lygChangePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyDateTo)).BeginInit();
            this.SuspendLayout();
            // 
            // lygDatePicker
            // 
            this.lygDatePicker.Controls.Add(this.dtDateTo);
            this.lygDatePicker.Controls.Add(this.dtDateFrom);
            this.lygDatePicker.Controls.Add(this.dteVoucherDate);
            this.lygDatePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lygDatePicker.Location = new System.Drawing.Point(4, 4);
            this.lygDatePicker.Name = "lygDatePicker";
            this.lygDatePicker.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(471, 327, 250, 350);
            this.lygDatePicker.Root = this.layoutControlGroup1;
            this.lygDatePicker.Size = new System.Drawing.Size(244, 152);
            this.lygDatePicker.TabIndex = 0;
            // 
            // dtDateTo
            // 
            this.dtDateTo.EditValue = null;
            this.dtDateTo.Location = new System.Drawing.Point(49, 121);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDateTo.Properties.Appearance.Options.UseFont = true;
            this.dtDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtDateTo.Size = new System.Drawing.Size(190, 26);
            this.dtDateTo.StyleController = this.lygDatePicker;
            this.dtDateTo.TabIndex = 7;
            this.dtDateTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtDateTo_KeyDown);
            // 
            // dtDateFrom
            // 
            this.dtDateFrom.EditValue = null;
            this.dtDateFrom.Location = new System.Drawing.Point(49, 87);
            this.dtDateFrom.Name = "dtDateFrom";
            this.dtDateFrom.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDateFrom.Properties.Appearance.Options.UseFont = true;
            this.dtDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtDateFrom.Size = new System.Drawing.Size(190, 26);
            this.dtDateFrom.StyleController = this.lygDatePicker;
            this.dtDateFrom.TabIndex = 6;
            this.dtDateFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtDateFrom_KeyDown);
            // 
            // dteVoucherDate
            // 
            this.dteVoucherDate.EditValue = new System.DateTime(2015, 5, 5, 16, 22, 12, 145);
            this.dteVoucherDate.Location = new System.Drawing.Point(9, 28);
            this.dteVoucherDate.Name = "dteVoucherDate";
            this.dteVoucherDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dteVoucherDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteVoucherDate.Properties.Appearance.Options.UseFont = true;
            this.dteVoucherDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dteVoucherDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteVoucherDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteVoucherDate.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "d";
            this.dteVoucherDate.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteVoucherDate.Properties.CalendarTimeProperties.EditFormat.FormatString = "d";
            this.dteVoucherDate.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteVoucherDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dteVoucherDate.Size = new System.Drawing.Size(226, 26);
            this.dteVoucherDate.StyleController = this.lygDatePicker;
            this.dteVoucherDate.TabIndex = 5;
            this.dteVoucherDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dteVoucherDate_KeyDown);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lygVoucherDate,
            this.lygChangePeriod});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(244, 152);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lygVoucherDate
            // 
            this.lygVoucherDate.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lygVoucherDate.AppearanceGroup.Options.UseFont = true;
            this.lygVoucherDate.CustomizationFormText = "Voucher Date";
            this.lygVoucherDate.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblDate});
            this.lygVoucherDate.Location = new System.Drawing.Point(0, 0);
            this.lygVoucherDate.Name = "lygVoucherDate";
            this.lygVoucherDate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lygVoucherDate.Size = new System.Drawing.Size(244, 63);
            this.lygVoucherDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lygVoucherDate.Text = "Voucher Date";
            // 
            // lblDate
            // 
            this.lblDate.AppearanceItemCaption.BorderColor = System.Drawing.Color.Black;
            this.lblDate.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.AppearanceItemCaption.Options.UseBorderColor = true;
            this.lblDate.AppearanceItemCaption.Options.UseFont = true;
            this.lblDate.Control = this.dteVoucherDate;
            this.lblDate.CustomizationFormText = "Date";
            this.lblDate.Location = new System.Drawing.Point(0, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.lblDate.Size = new System.Drawing.Size(242, 42);
            this.lblDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.lblDate.Text = "Date";
            this.lblDate.TextSize = new System.Drawing.Size(0, 0);
            this.lblDate.TextToControlDistance = 0;
            this.lblDate.TextVisible = false;
            // 
            // lygChangePeriod
            // 
            this.lygChangePeriod.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lygChangePeriod.AppearanceGroup.Options.UseFont = true;
            this.lygChangePeriod.CustomizationFormText = "Change Period";
            this.lygChangePeriod.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lyDateFrom,
            this.lyDateTo});
            this.lygChangePeriod.Location = new System.Drawing.Point(0, 63);
            this.lygChangePeriod.Name = "lygChangePeriod";
            this.lygChangePeriod.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lygChangePeriod.Size = new System.Drawing.Size(244, 89);
            this.lygChangePeriod.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lygChangePeriod.Text = "Change Period";
            // 
            // lyDateFrom
            // 
            this.lyDateFrom.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lyDateFrom.AppearanceItemCaption.Options.UseFont = true;
            this.lyDateFrom.Control = this.dtDateFrom;
            this.lyDateFrom.CustomizationFormText = "From";
            this.lyDateFrom.Location = new System.Drawing.Point(0, 0);
            this.lyDateFrom.Name = "lyDateFrom";
            this.lyDateFrom.Size = new System.Drawing.Size(242, 34);
            this.lyDateFrom.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lyDateFrom.Text = "From";
            this.lyDateFrom.TextSize = new System.Drawing.Size(41, 19);
            // 
            // lyDateTo
            // 
            this.lyDateTo.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lyDateTo.AppearanceItemCaption.Options.UseFont = true;
            this.lyDateTo.Control = this.dtDateTo;
            this.lyDateTo.CustomizationFormText = "To";
            this.lyDateTo.Location = new System.Drawing.Point(0, 34);
            this.lyDateTo.Name = "lyDateTo";
            this.lyDateTo.Size = new System.Drawing.Size(242, 34);
            this.lyDateTo.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lyDateTo.Text = "To";
            this.lyDateTo.TextSize = new System.Drawing.Size(41, 19);
            // 
            // frmDatePicker
            // 
            this.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.Appearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.Appearance.Options.UseBorderColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 161);
            this.Controls.Add(this.lygDatePicker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDatePicker";
            this.Padding = new System.Windows.Forms.Padding(4, 4, 5, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Date";
            this.Load += new System.EventHandler(this.frmDatePicker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lygDatePicker)).EndInit();
            this.lygDatePicker.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteVoucherDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteVoucherDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lygVoucherDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lygChangePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyDateTo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lygDatePicker;
        private DevExpress.XtraEditors.DateEdit dteVoucherDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lblDate;
        private DevExpress.XtraLayout.LayoutControlGroup lygVoucherDate;
        private DevExpress.XtraEditors.DateEdit dtDateTo;
        private DevExpress.XtraEditors.DateEdit dtDateFrom;
        private DevExpress.XtraLayout.LayoutControlGroup lygChangePeriod;
        private DevExpress.XtraLayout.LayoutControlItem lyDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem lyDateTo;
    }
}