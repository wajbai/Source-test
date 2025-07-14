namespace ACPP.Modules.UIControls
{
    partial class UcUsedCodesIcon
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pceUsedCodesIcon = new DevExpress.XtraEditors.PopupContainerEdit();
            this.lblLedgername = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pceUsedCodesIcon.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pceUsedCodesIcon
            // 
            this.pceUsedCodesIcon.Location = new System.Drawing.Point(3, 3);
            this.pceUsedCodesIcon.Name = "pceUsedCodesIcon";
            this.pceUsedCodesIcon.Properties.AllowFocused = false;
            this.pceUsedCodesIcon.Properties.AutoHeight = false;
            this.pceUsedCodesIcon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceUsedCodesIcon.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::ACPP.Properties.Resources.Used_Codes, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.pceUsedCodesIcon.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceUsedCodesIcon.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.pceUsedCodesIcon.Properties.UsePopupControlMinSize = true;
            this.pceUsedCodesIcon.Size = new System.Drawing.Size(23, 23);
            this.pceUsedCodesIcon.TabIndex = 0;
            this.pceUsedCodesIcon.ToolTip = "Click here to view all used Codes";
            this.pceUsedCodesIcon.Visible = false;
            this.pceUsedCodesIcon.Click += new System.EventHandler(this.pceUsedCodesIcon_Click);
            // 
            // lblLedgername
            // 
            this.lblLedgername.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblLedgername.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblLedgername.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.lblLedgername.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblLedgername.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lblLedgername.Location = new System.Drawing.Point(32, 7);
            this.lblLedgername.Name = "lblLedgername";
            this.lblLedgername.Size = new System.Drawing.Size(0, 0);
            this.lblLedgername.TabIndex = 1;
            // 
            // UcUsedCodesIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.lblLedgername);
            this.Controls.Add(this.pceUsedCodesIcon);
            this.Name = "UcUsedCodesIcon";
            this.Size = new System.Drawing.Size(512, 29);
            ((System.ComponentModel.ISupportInitialize)(this.pceUsedCodesIcon.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit pceUsedCodesIcon;
        private DevExpress.XtraEditors.LabelControl lblLedgername;

    }
}
