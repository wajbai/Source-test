namespace AcMEERPUpdater
{
    partial class frmUpdater
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
            this.pnlUpdaterInfo = new System.Windows.Forms.Panel();
            this.gvUpdatefileslist = new System.Windows.Forms.DataGridView();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblInstalledPathValue = new System.Windows.Forms.Label();
            this.lblUpdaterVersionValue = new System.Windows.Forms.Label();
            this.lblUpdateFiles = new System.Windows.Forms.Label();
            this.lblUpdaterVersion = new System.Windows.Forms.Label();
            this.lblVersionValue = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblNoofFile = new System.Windows.Forms.Label();
            this.lblInstalledPath = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblMultiDBDetails = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pbarUpdater = new System.Windows.Forms.ProgressBar();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cbbrancheslist = new System.Windows.Forms.ComboBox();
            this.btnMultiDBUpdate = new System.Windows.Forms.Button();
            this.btnMultiExit = new System.Windows.Forms.Button();
            this.pnlUpdaterInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvUpdatefileslist)).BeginInit();
            this.pnlStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlUpdaterInfo
            // 
            this.pnlUpdaterInfo.Controls.Add(this.gvUpdatefileslist);
            this.pnlUpdaterInfo.Controls.Add(this.btnUpdate);
            this.pnlUpdaterInfo.Controls.Add(this.btnExit);
            this.pnlUpdaterInfo.Controls.Add(this.lblInstalledPathValue);
            this.pnlUpdaterInfo.Controls.Add(this.lblUpdaterVersionValue);
            this.pnlUpdaterInfo.Controls.Add(this.lblUpdateFiles);
            this.pnlUpdaterInfo.Controls.Add(this.lblUpdaterVersion);
            this.pnlUpdaterInfo.Controls.Add(this.lblVersionValue);
            this.pnlUpdaterInfo.Controls.Add(this.lblVersion);
            this.pnlUpdaterInfo.Controls.Add(this.lblNoofFile);
            this.pnlUpdaterInfo.Controls.Add(this.lblInstalledPath);
            this.pnlUpdaterInfo.Location = new System.Drawing.Point(1, 1);
            this.pnlUpdaterInfo.Name = "pnlUpdaterInfo";
            this.pnlUpdaterInfo.Size = new System.Drawing.Size(457, 233);
            this.pnlUpdaterInfo.TabIndex = 5;
            this.pnlUpdaterInfo.Visible = false;
            // 
            // gvUpdatefileslist
            // 
            this.gvUpdatefileslist.AllowUserToAddRows = false;
            this.gvUpdatefileslist.AllowUserToDeleteRows = false;
            this.gvUpdatefileslist.AllowUserToResizeColumns = false;
            this.gvUpdatefileslist.AllowUserToResizeRows = false;
            this.gvUpdatefileslist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvUpdatefileslist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNo,
            this.File,
            this.Version,
            this.NewVersion});
            this.gvUpdatefileslist.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvUpdatefileslist.Location = new System.Drawing.Point(5, 106);
            this.gvUpdatefileslist.MultiSelect = false;
            this.gvUpdatefileslist.Name = "gvUpdatefileslist";
            this.gvUpdatefileslist.ReadOnly = true;
            this.gvUpdatefileslist.RowHeadersWidth = 20;
            this.gvUpdatefileslist.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvUpdatefileslist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvUpdatefileslist.Size = new System.Drawing.Size(448, 86);
            this.gvUpdatefileslist.TabIndex = 14;
            // 
            // SNo
            // 
            this.SNo.DataPropertyName = "SNo";
            this.SNo.HeaderText = "SNo";
            this.SNo.Name = "SNo";
            this.SNo.ReadOnly = true;
            this.SNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SNo.Width = 30;
            // 
            // File
            // 
            this.File.DataPropertyName = "File";
            this.File.HeaderText = "File";
            this.File.Name = "File";
            this.File.ReadOnly = true;
            this.File.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.File.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.File.Width = 200;
            // 
            // Version
            // 
            this.Version.DataPropertyName = "Version";
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Version.Width = 75;
            // 
            // NewVersion
            // 
            this.NewVersion.DataPropertyName = "NewVersion";
            this.NewVersion.HeaderText = "New Version";
            this.NewVersion.Name = "NewVersion";
            this.NewVersion.ReadOnly = true;
            this.NewVersion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NewVersion.Width = 75;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(297, 198);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(378, 198);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblInstalledPathValue
            // 
            this.lblInstalledPathValue.AutoSize = true;
            this.lblInstalledPathValue.Location = new System.Drawing.Point(120, 8);
            this.lblInstalledPathValue.Name = "lblInstalledPathValue";
            this.lblInstalledPathValue.Size = new System.Drawing.Size(71, 13);
            this.lblInstalledPathValue.TabIndex = 10;
            this.lblInstalledPathValue.Text = "Installed Path";
            // 
            // lblUpdaterVersionValue
            // 
            this.lblUpdaterVersionValue.AutoSize = true;
            this.lblUpdaterVersionValue.Location = new System.Drawing.Point(120, 55);
            this.lblUpdaterVersionValue.Name = "lblUpdaterVersionValue";
            this.lblUpdaterVersionValue.Size = new System.Drawing.Size(83, 13);
            this.lblUpdaterVersionValue.TabIndex = 11;
            this.lblUpdaterVersionValue.Text = "Updater Version";
            // 
            // lblUpdateFiles
            // 
            this.lblUpdateFiles.AutoSize = true;
            this.lblUpdateFiles.Location = new System.Drawing.Point(2, 85);
            this.lblUpdateFiles.Name = "lblUpdateFiles";
            this.lblUpdateFiles.Size = new System.Drawing.Size(197, 13);
            this.lblUpdateFiles.TabIndex = 9;
            this.lblUpdateFiles.Text = "The following(s) are going to be updated";
            // 
            // lblUpdaterVersion
            // 
            this.lblUpdaterVersion.AutoSize = true;
            this.lblUpdaterVersion.Location = new System.Drawing.Point(2, 54);
            this.lblUpdaterVersion.Name = "lblUpdaterVersion";
            this.lblUpdaterVersion.Size = new System.Drawing.Size(117, 13);
            this.lblUpdaterVersion.TabIndex = 4;
            this.lblUpdaterVersion.Text = "To be Updated Version";
            // 
            // lblVersionValue
            // 
            this.lblVersionValue.AutoSize = true;
            this.lblVersionValue.Location = new System.Drawing.Point(120, 31);
            this.lblVersionValue.Name = "lblVersionValue";
            this.lblVersionValue.Size = new System.Drawing.Size(42, 13);
            this.lblVersionValue.TabIndex = 6;
            this.lblVersionValue.Text = "Version";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(2, 30);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(79, 13);
            this.lblVersion.TabIndex = 8;
            this.lblVersion.Text = "Current Version";
            // 
            // lblNoofFile
            // 
            this.lblNoofFile.AutoSize = true;
            this.lblNoofFile.Location = new System.Drawing.Point(2, 195);
            this.lblNoofFile.Name = "lblNoofFile";
            this.lblNoofFile.Size = new System.Drawing.Size(104, 13);
            this.lblNoofFile.TabIndex = 7;
            this.lblNoofFile.Text = "No of files in updater";
            // 
            // lblInstalledPath
            // 
            this.lblInstalledPath.AutoSize = true;
            this.lblInstalledPath.Location = new System.Drawing.Point(2, 8);
            this.lblInstalledPath.Name = "lblInstalledPath";
            this.lblInstalledPath.Size = new System.Drawing.Size(71, 13);
            this.lblInstalledPath.TabIndex = 5;
            this.lblInstalledPath.Text = "Installed Path";
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.lblMultiDBDetails);
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Controls.Add(this.pbarUpdater);
            this.pnlStatus.Location = new System.Drawing.Point(1, 228);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(457, 55);
            this.pnlStatus.TabIndex = 7;
            this.pnlStatus.Visible = false;
            // 
            // lblMultiDBDetails
            // 
            this.lblMultiDBDetails.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblMultiDBDetails.Location = new System.Drawing.Point(230, 19);
            this.lblMultiDBDetails.Name = "lblMultiDBDetails";
            this.lblMultiDBDetails.Size = new System.Drawing.Size(223, 36);
            this.lblMultiDBDetails.TabIndex = 13;
            this.lblMultiDBDetails.Text = "There are <x> branches are available";
            this.lblMultiDBDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(3, 35);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(102, 14);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status of updater";
            // 
            // pbarUpdater
            // 
            this.pbarUpdater.Location = new System.Drawing.Point(4, 6);
            this.pbarUpdater.Name = "pbarUpdater";
            this.pbarUpdater.Size = new System.Drawing.Size(448, 25);
            this.pbarUpdater.TabIndex = 7;
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBranch.Location = new System.Drawing.Point(78, 294);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(45, 14);
            this.lblBranch.TabIndex = 12;
            this.lblBranch.Text = "Branch";
            // 
            // cbbrancheslist
            // 
            this.cbbrancheslist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbrancheslist.FormattingEnabled = true;
            this.cbbrancheslist.Location = new System.Drawing.Point(128, 290);
            this.cbbrancheslist.Name = "cbbrancheslist";
            this.cbbrancheslist.Size = new System.Drawing.Size(173, 21);
            this.cbbrancheslist.TabIndex = 11;
            this.cbbrancheslist.SelectedIndexChanged += new System.EventHandler(this.cbbrancheslist_SelectedIndexChanged);
            // 
            // btnMultiDBUpdate
            // 
            this.btnMultiDBUpdate.Location = new System.Drawing.Point(302, 289);
            this.btnMultiDBUpdate.Name = "btnMultiDBUpdate";
            this.btnMultiDBUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnMultiDBUpdate.TabIndex = 13;
            this.btnMultiDBUpdate.Text = "&Update";
            this.btnMultiDBUpdate.UseVisualStyleBackColor = true;
            this.btnMultiDBUpdate.Click += new System.EventHandler(this.btnMultiDBUpdate_Click);
            // 
            // btnMultiExit
            // 
            this.btnMultiExit.Location = new System.Drawing.Point(379, 289);
            this.btnMultiExit.Name = "btnMultiExit";
            this.btnMultiExit.Size = new System.Drawing.Size(75, 23);
            this.btnMultiExit.TabIndex = 14;
            this.btnMultiExit.Text = "&Exit";
            this.btnMultiExit.UseVisualStyleBackColor = true;
            this.btnMultiExit.Click += new System.EventHandler(this.btnMultiExit_Click);
            // 
            // frmUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(459, 322);
            this.Controls.Add(this.btnMultiExit);
            this.Controls.Add(this.btnMultiDBUpdate);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.cbbrancheslist);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlUpdaterInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AcMEERP Updater";
            this.Shown += new System.EventHandler(this.frmUpdater_Shown);
            this.pnlUpdaterInfo.ResumeLayout(false);
            this.pnlUpdaterInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvUpdatefileslist)).EndInit();
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlUpdaterInfo;
        private System.Windows.Forms.DataGridView gvUpdatefileslist;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn File;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewVersion;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblInstalledPathValue;
        private System.Windows.Forms.Label lblUpdaterVersionValue;
        private System.Windows.Forms.Label lblUpdateFiles;
        private System.Windows.Forms.Label lblUpdaterVersion;
        private System.Windows.Forms.Label lblVersionValue;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblNoofFile;
        private System.Windows.Forms.Label lblInstalledPath;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar pbarUpdater;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cbbrancheslist;
        private System.Windows.Forms.Button btnMultiDBUpdate;
        private System.Windows.Forms.Label lblMultiDBDetails;
        private System.Windows.Forms.Button btnMultiExit;

    }
}

