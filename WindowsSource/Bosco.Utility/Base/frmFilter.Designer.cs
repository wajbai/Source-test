namespace Bosco.Utility.Base
{
    partial class frmFilter
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
            this.lcFilter = new DevExpress.XtraLayout.LayoutControl();
            this.filterctl = new Bosco.Utility.Controls.FilterControl.CustomFilterControl();
            this.lblNote = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.lcgrpFilter = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcFilter)).BeginInit();
            this.lcFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcgrpFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // lcFilter
            // 
            this.lcFilter.Controls.Add(this.filterctl);
            this.lcFilter.Controls.Add(this.lblNote);
            this.lcFilter.Controls.Add(this.btnCancel);
            this.lcFilter.Controls.Add(this.btnClear);
            this.lcFilter.Controls.Add(this.btnOk);
            this.lcFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcFilter.Location = new System.Drawing.Point(0, 0);
            this.lcFilter.Name = "lcFilter";
            this.lcFilter.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(582, 207, 250, 350);
            this.lcFilter.Root = this.lcgrpFilter;
            this.lcFilter.Size = new System.Drawing.Size(594, 283);
            this.lcFilter.TabIndex = 0;
            this.lcFilter.Text = "layoutControl1";
            // 
            // filterctl
            // 
            this.filterctl.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.filterctl.Location = new System.Drawing.Point(7, 7);
            this.filterctl.Name = "filterctl";
            this.filterctl.Size = new System.Drawing.Size(580, 237);
            this.filterctl.TabIndex = 23;
            this.filterctl.Text = "filterControl1";
            // 
            // lblNote
            // 
            this.lblNote.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblNote.Appearance.Image = global::Bosco.Utility.Properties.Resources.info;
            this.lblNote.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNote.Appearance.PressedImage = global::Bosco.Utility.Properties.Resources.info;
            this.lblNote.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblNote.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblNote.LineLocation = DevExpress.XtraEditors.LineLocation.Bottom;
            this.lblNote.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal;
            this.lblNote.LineVisible = true;
            this.lblNote.Location = new System.Drawing.Point(7, 248);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(355, 28);
            this.lblNote.StyleController = this.lcFilter;
            this.lblNote.TabIndex = 22;
            this.lblNote.Text = "Press the Enter key to select fields/conditions.  Use Arrow, Insert and Delete ke" +
                "ys to manage conditions";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(519, 251);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 22);
            this.btnCancel.StyleController = this.lcFilter;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnClear.Location = new System.Drawing.Point(369, 251);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(65, 22);
            this.btnClear.StyleController = this.lcFilter;
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOk.Location = new System.Drawing.Point(444, 251);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 22);
            this.btnOk.StyleController = this.lcFilter;
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lcgrpFilter
            // 
            this.lcgrpFilter.CustomizationFormText = "Root";
            this.lcgrpFilter.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgrpFilter.GroupBordersVisible = false;
            this.lcgrpFilter.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.lcgrpFilter.Location = new System.Drawing.Point(0, 0);
            this.lcgrpFilter.Name = "Root";
            this.lcgrpFilter.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgrpFilter.Size = new System.Drawing.Size(594, 283);
            this.lcgrpFilter.Text = "Root";
            this.lcgrpFilter.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnOk;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(434, 241);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(75, 32);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(75, 32);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(75, 32);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClear;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(359, 241);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(75, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(75, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(75, 32);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(509, 241);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(75, 32);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(75, 32);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(75, 32);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblNote;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 241);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(359, 32);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(359, 32);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(359, 32);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.filterctl;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(584, 241);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(594, 283);
            this.Controls.Add(this.lcFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter";
            this.Activated += new System.EventHandler(this.frmFilter_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.lcFilter)).EndInit();
            this.lcFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcgrpFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcFilter;
        private DevExpress.XtraLayout.LayoutControlGroup lcgrpFilter;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        //private DevExpress.XtraEditors.FilterControl filterctl;
        private Controls.FilterControl.CustomFilterControl filterctl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}