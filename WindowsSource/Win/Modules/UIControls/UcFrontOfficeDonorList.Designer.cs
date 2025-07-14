namespace ACPP.Modules.UIControls
{
    partial class UcFrontOfficeDonorList
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
            this.gcDonor = new DevExpress.XtraGrid.GridControl();
            this.gvDonor = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gcDonor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonor)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDonor
            // 
            this.gcDonor.Location = new System.Drawing.Point(3, 3);
            this.gcDonor.MainView = this.gvDonor;
            this.gcDonor.Name = "gcDonor";
            this.gcDonor.Size = new System.Drawing.Size(619, 313);
            this.gcDonor.TabIndex = 0;
            this.gcDonor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDonor});
            // 
            // gvDonor
            // 
            this.gvDonor.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvDonor.Appearance.FocusedRow.Options.UseFont = true;
            this.gvDonor.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvDonor.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvDonor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvDonor.GridControl = this.gcDonor;
            this.gvDonor.Name = "gvDonor";
            this.gvDonor.OptionsBehavior.Editable = false;
            this.gvDonor.OptionsBehavior.ReadOnly = true;
            this.gvDonor.OptionsFind.AllowFindPanel = false;
            this.gvDonor.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvDonor.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvDonor.OptionsSelection.MultiSelect = true;
            this.gvDonor.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvDonor.OptionsView.ShowGroupPanel = false;
            this.gvDonor.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvDonor_RowStyle);
            // 
            // UcFrontOfficeDonorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcDonor);
            this.Name = "UcFrontOfficeDonorList";
            this.Size = new System.Drawing.Size(625, 322);
            ((System.ComponentModel.ISupportInitialize)(this.gcDonor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDonor;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDonor;
    }
}