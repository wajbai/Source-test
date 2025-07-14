namespace ACPP.Modules.UIControls
{
    partial class ucDatePicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblVoucherDate = new DevExpress.XtraEditors.LabelControl();
            this.lygDatePicker = new DevExpress.XtraLayout.LayoutControlGroup();
            this.locVoucherDateLable = new DevExpress.XtraLayout.LayoutControlItem();
            this.locDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.datVoucherDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lygDatePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locVoucherDateLable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datVoucherDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datVoucherDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblVoucherDate);
            this.layoutControl1.Controls.Add(this.datVoucherDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(521, 160, 250, 350);
            this.layoutControl1.Root = this.lygDatePicker;
            this.layoutControl1.Size = new System.Drawing.Size(102, 46);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblVoucherDate
            // 
            this.lblVoucherDate.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblVoucherDate.Location = new System.Drawing.Point(5, 5);
            this.lblVoucherDate.Name = "lblVoucherDate";
            this.lblVoucherDate.Size = new System.Drawing.Size(27, 13);
            this.lblVoucherDate.StyleController = this.layoutControl1;
            this.lblVoucherDate.TabIndex = 5;
            this.lblVoucherDate.Text = "Date";
            // 
            // lygDatePicker
            // 
            this.lygDatePicker.ContentImageAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.lygDatePicker.CustomizationFormText = "Root";
            this.lygDatePicker.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lygDatePicker.GroupBordersVisible = false;
            this.lygDatePicker.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.locVoucherDateLable,
            this.locDate});
            this.lygDatePicker.Location = new System.Drawing.Point(0, 0);
            this.lygDatePicker.Name = "lygDatePicker";
            this.lygDatePicker.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.lygDatePicker.Size = new System.Drawing.Size(102, 46);
            this.lygDatePicker.Text = "lygDatePicker";
            this.lygDatePicker.TextVisible = false;
            // 
            // locVoucherDateLable
            // 
            this.locVoucherDateLable.Control = this.lblVoucherDate;
            this.locVoucherDateLable.CustomizationFormText = "VoucherDate";
            this.locVoucherDateLable.Location = new System.Drawing.Point(0, 0);
            this.locVoucherDateLable.Name = "locVoucherDateLable";
            this.locVoucherDateLable.Size = new System.Drawing.Size(96, 17);
            this.locVoucherDateLable.Text = "VoucherDate";
            this.locVoucherDateLable.TextSize = new System.Drawing.Size(0, 0);
            this.locVoucherDateLable.TextToControlDistance = 0;
            this.locVoucherDateLable.TextVisible = false;
            // 
            // locDate
            // 
            this.locDate.Control = this.datVoucherDate;
            this.locDate.CustomizationFormText = "locDate";
            this.locDate.Location = new System.Drawing.Point(0, 17);
            this.locDate.Name = "locDate";
            this.locDate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.locDate.Size = new System.Drawing.Size(96, 23);
            this.locDate.Text = "locDate";
            this.locDate.TextSize = new System.Drawing.Size(0, 0);
            this.locDate.TextToControlDistance = 0;
            this.locDate.TextVisible = false;
            // 
            // datVoucherDate
            // 
            this.datVoucherDate.EditValue = null;
            this.datVoucherDate.Location = new System.Drawing.Point(3, 20);
            this.datVoucherDate.Name = "datVoucherDate";
            this.datVoucherDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datVoucherDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datVoucherDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.datVoucherDate.Size = new System.Drawing.Size(96, 20);
            this.datVoucherDate.StyleController = this.layoutControl1;
            this.datVoucherDate.TabIndex = 4;
            this.datVoucherDate.MouseEnter += new System.EventHandler(this.datVoucherDate_MouseEnter);
            // 
            // ucDatePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucDatePicker";
            this.Size = new System.Drawing.Size(102, 46);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lygDatePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locVoucherDateLable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datVoucherDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datVoucherDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LabelControl lblVoucherDate;
        private DevExpress.XtraLayout.LayoutControlGroup lygDatePicker;
        private DevExpress.XtraLayout.LayoutControlItem locVoucherDateLable;
        private DevExpress.XtraEditors.DateEdit datVoucherDate;
        private DevExpress.XtraLayout.LayoutControlItem locDate;
    }
}
