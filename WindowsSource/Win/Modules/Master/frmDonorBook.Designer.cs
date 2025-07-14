namespace ACPP.Modules.Master
{
    partial class frmDonorBook
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
            this.pnlDonorBooktop = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarDonorBook = new ACPP.Modules.UIControls.ucToolBar();
            this.pnlDonorBookfooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblAddressView = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new System.Windows.Forms.CheckBox();
            this.pnlAuditorBookfill = new DevExpress.XtraEditors.PanelControl();
            this.gcDonorBookView = new DevExpress.XtraGrid.GridControl();
            this.gvDonorBook = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDonorId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPinCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colURL = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDonorBooktop)).BeginInit();
            this.pnlDonorBooktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDonorBookfooter)).BeginInit();
            this.pnlDonorBookfooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuditorBookfill)).BeginInit();
            this.pnlAuditorBookfill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDonorBookView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonorBook)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDonorBooktop
            // 
            this.pnlDonorBooktop.Controls.Add(this.ucToolBarDonorBook);
            this.pnlDonorBooktop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDonorBooktop.Location = new System.Drawing.Point(5, 5);
            this.pnlDonorBooktop.Name = "pnlDonorBooktop";
            this.pnlDonorBooktop.Size = new System.Drawing.Size(741, 37);
            this.pnlDonorBooktop.TabIndex = 0;
            // 
            // ucToolBarDonorBook
            // 
            this.ucToolBarDonorBook.DisableAddButton = true;
            this.ucToolBarDonorBook.DisableCloseButton = true;
            this.ucToolBarDonorBook.DisableDeleteButton = true;
            this.ucToolBarDonorBook.DisableEditButton = true;
            this.ucToolBarDonorBook.DisablePrintButton = true;
            this.ucToolBarDonorBook.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucToolBarDonorBook.Location = new System.Drawing.Point(2, 2);
            this.ucToolBarDonorBook.Name = "ucToolBarDonorBook";
            this.ucToolBarDonorBook.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.ucToolBarDonorBook.ShowHTML = true;
            this.ucToolBarDonorBook.ShowMMT = true;
            this.ucToolBarDonorBook.ShowPDF = true;
            this.ucToolBarDonorBook.ShowRTF = true;
            this.ucToolBarDonorBook.ShowText = true;
            this.ucToolBarDonorBook.ShowXLS = true;
            this.ucToolBarDonorBook.ShowXLSX = true;
            this.ucToolBarDonorBook.Size = new System.Drawing.Size(737, 40);
            this.ucToolBarDonorBook.TabIndex = 0;
            this.ucToolBarDonorBook.AddClicked += new System.EventHandler(this.ucToolBarDonorBook_AddClicked);
            this.ucToolBarDonorBook.EditClicked += new System.EventHandler(this.ucToolBarDonorBook_EditClicked);
            this.ucToolBarDonorBook.DeleteClicked += new System.EventHandler(this.ucToolBarDonorBook_DeleteClicked);
            this.ucToolBarDonorBook.PrintClicked += new System.EventHandler(this.ucToolBarDonorBook_PrintClicked);
            this.ucToolBarDonorBook.CloseClicked += new System.EventHandler(this.ucToolBarDonorBook_CloseClicked);
            // 
            // pnlDonorBookfooter
            // 
            this.pnlDonorBookfooter.Controls.Add(this.lblRecordCount);
            this.pnlDonorBookfooter.Controls.Add(this.lblAddressView);
            this.pnlDonorBookfooter.Controls.Add(this.chkShowFilter);
            this.pnlDonorBookfooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDonorBookfooter.Location = new System.Drawing.Point(5, 342);
            this.pnlDonorBookfooter.Name = "pnlDonorBookfooter";
            this.pnlDonorBookfooter.Size = new System.Drawing.Size(741, 27);
            this.pnlDonorBookfooter.TabIndex = 2;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRecordCount.Location = new System.Drawing.Point(721, 7);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(7, 13);
            this.lblRecordCount.TabIndex = 9;
            this.lblRecordCount.Text = "0";
            // 
            // lblAddressView
            // 
            this.lblAddressView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAddressView.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAddressView.Location = new System.Drawing.Point(705, 7);
            this.lblAddressView.Name = "lblAddressView";
            this.lblAddressView.Size = new System.Drawing.Size(9, 13);
            this.lblAddressView.TabIndex = 8;
            this.lblAddressView.Text = "#";
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.AutoSize = true;
            this.chkShowFilter.Location = new System.Drawing.Point(6, 6);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Size = new System.Drawing.Size(76, 17);
            this.chkShowFilter.TabIndex = 3;
            this.chkShowFilter.Text = "ShowFilter";
            this.chkShowFilter.UseVisualStyleBackColor = true;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // pnlAuditorBookfill
            // 
            this.pnlAuditorBookfill.Controls.Add(this.gcDonorBookView);
            this.pnlAuditorBookfill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAuditorBookfill.Location = new System.Drawing.Point(5, 42);
            this.pnlAuditorBookfill.Name = "pnlAuditorBookfill";
            this.pnlAuditorBookfill.Size = new System.Drawing.Size(741, 300);
            this.pnlAuditorBookfill.TabIndex = 3;
            // 
            // gcDonorBookView
            // 
            this.gcDonorBookView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDonorBookView.Location = new System.Drawing.Point(2, 2);
            this.gcDonorBookView.MainView = this.gvDonorBook;
            this.gcDonorBookView.Name = "gcDonorBookView";
            this.gcDonorBookView.Size = new System.Drawing.Size(737, 296);
            this.gcDonorBookView.TabIndex = 0;
            this.gcDonorBookView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDonorBook});
            // 
            // gvDonorBook
            // 
            this.gvDonorBook.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDonorId,
            this.colName,
            this.colAddress,
            this.colPlace,
            this.colState,
            this.colCountry,
            this.colPinCode,
            this.colPhone,
            this.colFax,
            this.colEmail,
            this.colURL});
            this.gvDonorBook.GridControl = this.gcDonorBookView;
            this.gvDonorBook.Name = "gvDonorBook";
            this.gvDonorBook.OptionsBehavior.Editable = false;
            this.gvDonorBook.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvDonorBook.OptionsView.ShowGroupPanel = false;
            this.gvDonorBook.DoubleClick += new System.EventHandler(this.gvDonorBook_DoubleClick);
            this.gvDonorBook.RowCountChanged += new System.EventHandler(this.gvDonorBook_RowCountChanged);
            // 
            // colDonorId
            // 
            this.colDonorId.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDonorId.AppearanceHeader.Options.UseFont = true;
            this.colDonorId.Caption = "DonorId";
            this.colDonorId.FieldName = "DONAUD_ID";
            this.colDonorId.Name = "colDonorId";
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.Caption = "Name";
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // colAddress
            // 
            this.colAddress.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAddress.AppearanceHeader.Options.UseFont = true;
            this.colAddress.Caption = "Address";
            this.colAddress.FieldName = "ADDRESS";
            this.colAddress.Name = "colAddress";
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 9;
            // 
            // colPlace
            // 
            this.colPlace.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colPlace.AppearanceHeader.Options.UseFont = true;
            this.colPlace.Caption = "Place";
            this.colPlace.FieldName = "PLACE";
            this.colPlace.Name = "colPlace";
            this.colPlace.Visible = true;
            this.colPlace.VisibleIndex = 1;
            // 
            // colState
            // 
            this.colState.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colState.AppearanceHeader.Options.UseFont = true;
            this.colState.Caption = "State";
            this.colState.FieldName = "STATE";
            this.colState.Name = "colState";
            this.colState.Visible = true;
            this.colState.VisibleIndex = 8;
            // 
            // colCountry
            // 
            this.colCountry.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCountry.AppearanceHeader.Options.UseFont = true;
            this.colCountry.Caption = "Country";
            this.colCountry.FieldName = "COUNTRY";
            this.colCountry.Name = "colCountry";
            this.colCountry.Visible = true;
            this.colCountry.VisibleIndex = 2;
            // 
            // colPinCode
            // 
            this.colPinCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colPinCode.AppearanceHeader.Options.UseFont = true;
            this.colPinCode.Caption = "Pin Code";
            this.colPinCode.FieldName = "PINCODE";
            this.colPinCode.Name = "colPinCode";
            this.colPinCode.Visible = true;
            this.colPinCode.VisibleIndex = 3;
            // 
            // colPhone
            // 
            this.colPhone.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colPhone.AppearanceHeader.Options.UseFont = true;
            this.colPhone.Caption = "Phone";
            this.colPhone.FieldName = "PHONE";
            this.colPhone.Name = "colPhone";
            this.colPhone.Visible = true;
            this.colPhone.VisibleIndex = 4;
            // 
            // colFax
            // 
            this.colFax.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFax.AppearanceHeader.Options.UseFont = true;
            this.colFax.Caption = "Fax";
            this.colFax.FieldName = "FAX";
            this.colFax.Name = "colFax";
            this.colFax.Visible = true;
            this.colFax.VisibleIndex = 5;
            // 
            // colEmail
            // 
            this.colEmail.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.colEmail.AppearanceHeader.Options.UseFont = true;
            this.colEmail.Caption = "EMail";
            this.colEmail.FieldName = "EMAIL";
            this.colEmail.Name = "colEmail";
            this.colEmail.Visible = true;
            this.colEmail.VisibleIndex = 6;
            // 
            // colURL
            // 
            this.colURL.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colURL.AppearanceHeader.Options.UseFont = true;
            this.colURL.Caption = "URL";
            this.colURL.FieldName = "URL";
            this.colURL.Name = "colURL";
            this.colURL.Visible = true;
            this.colURL.VisibleIndex = 7;
            // 
            // frmDonorBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 374);
            this.Controls.Add(this.pnlAuditorBookfill);
            this.Controls.Add(this.pnlDonorBookfooter);
            this.Controls.Add(this.pnlDonorBooktop);
            this.Name = "frmDonorBook";
            this.Text = "Donor Book";
            this.Load += new System.EventHandler(this.frmDonorBook_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlDonorBooktop)).EndInit();
            this.pnlDonorBooktop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlDonorBookfooter)).EndInit();
            this.pnlDonorBookfooter.ResumeLayout(false);
            this.pnlDonorBookfooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuditorBookfill)).EndInit();
            this.pnlAuditorBookfill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDonorBookView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonorBook)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlDonorBooktop;
        private DevExpress.XtraEditors.PanelControl pnlDonorBookfooter;
        private UIControls.ucToolBar ucToolBarDonorBook;
        private DevExpress.XtraEditors.PanelControl pnlAuditorBookfill;
        private DevExpress.XtraGrid.GridControl gcDonorBookView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDonorBook;
        private System.Windows.Forms.CheckBox chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblAddressView;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraGrid.Columns.GridColumn colDonorId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colPlace;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colPinCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colFax;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colURL;
    }
}