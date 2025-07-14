namespace ACPP.Modules.UIControls
{
    partial class ucFDRealization
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
            this.gcFDRealizatiion = new DevExpress.XtraGrid.GridControl();
            this.gvFDRealization = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFDAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDBankAccountNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDMaturityDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcFDRealizatiion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFDRealization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcFDRealizatiion
            // 
            this.gcFDRealizatiion.Location = new System.Drawing.Point(0, 4);
            this.gcFDRealizatiion.MainView = this.gvFDRealization;
            this.gcFDRealizatiion.Name = "gcFDRealizatiion";
            this.gcFDRealizatiion.Size = new System.Drawing.Size(400, 200);
            this.gcFDRealizatiion.TabIndex = 0;
            this.gcFDRealizatiion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFDRealization});
            //this.gcFDRealizatiion.Click += new System.EventHandler(this.gcFDRealizatiion_Click);
            // 
            // gvFDRealization
            // 
            this.gvFDRealization.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFDRealization.Appearance.FocusedCell.Options.UseFont = true;
            this.gvFDRealization.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFDRealization.Appearance.FocusedRow.Options.UseFont = true;
            this.gvFDRealization.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFDRealization.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvFDRealization.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFDAccountId,
            this.colFDBankAccountNo,
            this.colFDMaturityDate});
            this.gvFDRealization.GridControl = this.gcFDRealizatiion;
            this.gvFDRealization.Name = "gvFDRealization";
            this.gvFDRealization.OptionsBehavior.Editable = false;
            this.gvFDRealization.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvFDRealization.OptionsView.ShowGroupPanel = false;
            this.gvFDRealization.OptionsView.ShowIndicator = false;
            // 
            // colFDAccountId
            // 
            this.colFDAccountId.Caption = "FDAccountId";
            this.colFDAccountId.FieldName = "FD_ACCOUNT_ID";
            this.colFDAccountId.Name = "colFDAccountId";
            // 
            // colFDBankAccountNo
            // 
            this.colFDBankAccountNo.Caption = "FD Account No";
            this.colFDBankAccountNo.FieldName = "FD_ACCOUNT_NUMBER";
            this.colFDBankAccountNo.Name = "colFDBankAccountNo";
            this.colFDBankAccountNo.Visible = true;
            this.colFDBankAccountNo.VisibleIndex = 0;
            // 
            // colFDMaturityDate
            // 
            this.colFDMaturityDate.Caption = "FD Maturity Date";
            this.colFDMaturityDate.FieldName = "MATURITY_DATE";
            this.colFDMaturityDate.Name = "colFDMaturityDate";
            this.colFDMaturityDate.Visible = true;
            this.colFDMaturityDate.VisibleIndex = 1;
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.gcFDRealizatiion);
            this.popupContainerControl1.Location = new System.Drawing.Point(3, 3);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(402, 204);
            this.popupContainerControl1.TabIndex = 1;
            // 
            // ucFDRealization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerControl1);
            this.Name = "ucFDRealization";
            this.Size = new System.Drawing.Size(405, 211);
            ((System.ComponentModel.ISupportInitialize)(this.gcFDRealizatiion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFDRealization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcFDRealizatiion;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFDRealization;
        private DevExpress.XtraGrid.Columns.GridColumn colFDBankAccountNo;
        private DevExpress.XtraGrid.Columns.GridColumn colFDMaturityDate;
        private DevExpress.XtraGrid.Columns.GridColumn colFDAccountId;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
    }
}
