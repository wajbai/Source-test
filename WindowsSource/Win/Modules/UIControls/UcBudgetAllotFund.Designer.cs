namespace ACPP.Modules.UIControls
{
    partial class UcBudgetAllotFund
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcBudgetAllotFund));
            this.lblLableName = new DevExpress.XtraEditors.LabelControl();
            this.txtCtrl = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtrl.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLableName
            // 
            resources.ApplyResources(this.lblLableName, "lblLableName");
            this.lblLableName.Name = "lblLableName";
            // 
            // txtCtrl
            // 
            resources.ApplyResources(this.txtCtrl, "txtCtrl");
            this.txtCtrl.Name = "txtCtrl";
            this.txtCtrl.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCtrl.Properties.Mask.EditMask = resources.GetString("txtCtrl.Properties.Mask.EditMask");
            this.txtCtrl.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtCtrl.Properties.Mask.MaskType")));
            this.txtCtrl.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("txtCtrl.Properties.Mask.UseMaskAsDisplayFormat")));
            // 
            // UcBudgetAllotFund
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtCtrl);
            this.Controls.Add(this.lblLableName);
            this.Name = "UcBudgetAllotFund";
            ((System.ComponentModel.ISupportInitialize)(this.txtCtrl.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblLableName;
        public DevExpress.XtraEditors.TextEdit txtCtrl;

    }
}
