namespace Acme.erpSupport
{
    partial class frmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlAcmeerp = new System.Windows.Forms.Panel();
            this.lblLocationValue = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.gvLicense = new System.Windows.Forms.DataGridView();
            this.richtxtResults = new System.Windows.Forms.RichTextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblPortValue = new System.Windows.Forms.Label();
            this.lblDBNameValue = new System.Windows.Forms.Label();
            this.lblDBserverValue = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblBranch = new System.Windows.Forms.Label();
            this.lblDBName = new System.Windows.Forms.Label();
            this.lblVersionValue = new System.Windows.Forms.Label();
            this.lblDBserver = new System.Windows.Forms.Label();
            this.lblPathValue = new System.Windows.Forms.Label();
            this.lbllicenseInfo = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.cboAction = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblLocalBOName = new System.Windows.Forms.Label();
            this.gboxDeleteProject = new System.Windows.Forms.GroupBox();
            this.lblDeleteProject = new System.Windows.Forms.Label();
            this.btnRemoveProject = new System.Windows.Forms.Button();
            this.cboProject = new System.Windows.Forms.ComboBox();
            this.gboxDeleteFD = new System.Windows.Forms.GroupBox();
            this.lblFDProject = new System.Windows.Forms.Label();
            this.cbFDProject = new System.Windows.Forms.ComboBox();
            this.lblFD = new System.Windows.Forms.Label();
            this.btnFDDelete = new System.Windows.Forms.Button();
            this.cboFD = new System.Windows.Forms.ComboBox();
            this.gboxConnectionString = new System.Windows.Forms.GroupBox();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.btnConnectionStringUpdate = new System.Windows.Forms.Button();
            this.gboxAcmeerpService = new System.Windows.Forms.GroupBox();
            this.btnINIOpen = new System.Windows.Forms.Button();
            this.lblServiceINIValue = new System.Windows.Forms.Label();
            this.lblServiceINI = new System.Windows.Forms.Label();
            this.lblMySQLInstalledPathValue = new System.Windows.Forms.Label();
            this.btnCreateService = new System.Windows.Forms.Button();
            this.btnRemoveService = new System.Windows.Forms.Button();
            this.btnServiceAction = new System.Windows.Forms.Button();
            this.lblServiceStatusValue = new System.Windows.Forms.Label();
            this.lblServiceStatus = new System.Windows.Forms.Label();
            this.lblServicePortValue = new System.Windows.Forms.Label();
            this.lblMySQLInstalledPath = new System.Windows.Forms.Label();
            this.lblServiceInstalled = new System.Windows.Forms.Label();
            this.lblServicePort = new System.Windows.Forms.Label();
            this.lblServiceInstalledValue = new System.Windows.Forms.Label();
            this.gboxDeleteFDAll = new System.Windows.Forms.GroupBox();
            this.lblAllFDConfirmationMessge = new System.Windows.Forms.Label();
            this.btnDeleteAllFDs = new System.Windows.Forms.Button();
            this.lblRefreshMessage = new System.Windows.Forms.Label();
            this.gboxUpdateLocation = new System.Windows.Forms.GroupBox();
            this.lblLicenseLocations = new System.Windows.Forms.Label();
            this.btnUpdateLocation = new System.Windows.Forms.Button();
            this.cboLocations = new System.Windows.Forms.ComboBox();
            this.gboxResetDBVouchers = new System.Windows.Forms.GroupBox();
            this.chkClearOP = new System.Windows.Forms.CheckBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.dateBooksBegin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.btnResetDBVoucher = new System.Windows.Forms.Button();
            this.gboxRestoreDatabase = new System.Windows.Forms.GroupBox();
            this.lblNewDB = new System.Windows.Forms.Label();
            this.txtNewSchema = new System.Windows.Forms.TextBox();
            this.btnFileBrowse = new System.Windows.Forms.Button();
            this.txtDBbackupPath = new System.Windows.Forms.TextBox();
            this.lblDBBackupPath = new System.Windows.Forms.Label();
            this.btnDBRestore = new System.Windows.Forms.Button();
            this.openFileDBBackup = new System.Windows.Forms.OpenFileDialog();
            this.pnlAcmeerp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLicense)).BeginInit();
            this.gboxDeleteProject.SuspendLayout();
            this.gboxDeleteFD.SuspendLayout();
            this.gboxConnectionString.SuspendLayout();
            this.gboxAcmeerpService.SuspendLayout();
            this.gboxDeleteFDAll.SuspendLayout();
            this.gboxUpdateLocation.SuspendLayout();
            this.gboxResetDBVouchers.SuspendLayout();
            this.gboxRestoreDatabase.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(550, 445);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlAcmeerp
            // 
            this.pnlAcmeerp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAcmeerp.Controls.Add(this.lblLocationValue);
            this.pnlAcmeerp.Controls.Add(this.lblLocation);
            this.pnlAcmeerp.Controls.Add(this.gvLicense);
            this.pnlAcmeerp.Controls.Add(this.richtxtResults);
            this.pnlAcmeerp.Controls.Add(this.lblResult);
            this.pnlAcmeerp.Controls.Add(this.lblPortValue);
            this.pnlAcmeerp.Controls.Add(this.lblDBNameValue);
            this.pnlAcmeerp.Controls.Add(this.lblDBserverValue);
            this.pnlAcmeerp.Controls.Add(this.lblPort);
            this.pnlAcmeerp.Controls.Add(this.lblBranch);
            this.pnlAcmeerp.Controls.Add(this.lblDBName);
            this.pnlAcmeerp.Controls.Add(this.lblVersionValue);
            this.pnlAcmeerp.Controls.Add(this.lblDBserver);
            this.pnlAcmeerp.Controls.Add(this.lblPathValue);
            this.pnlAcmeerp.Controls.Add(this.lbllicenseInfo);
            this.pnlAcmeerp.Controls.Add(this.lblVersion);
            this.pnlAcmeerp.Controls.Add(this.lblPath);
            this.pnlAcmeerp.Controls.Add(this.lblAction);
            this.pnlAcmeerp.Controls.Add(this.cboBranch);
            this.pnlAcmeerp.Controls.Add(this.cboAction);
            this.pnlAcmeerp.Controls.Add(this.btnOk);
            this.pnlAcmeerp.Location = new System.Drawing.Point(5, 4);
            this.pnlAcmeerp.Name = "pnlAcmeerp";
            this.pnlAcmeerp.Size = new System.Drawing.Size(620, 229);
            this.pnlAcmeerp.TabIndex = 0;
            // 
            // lblLocationValue
            // 
            this.lblLocationValue.AutoSize = true;
            this.lblLocationValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationValue.Location = new System.Drawing.Point(488, 43);
            this.lblLocationValue.Name = "lblLocationValue";
            this.lblLocationValue.Size = new System.Drawing.Size(48, 13);
            this.lblLocationValue.TabIndex = 20;
            this.lblLocationValue.Text = "Location";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(436, 43);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(56, 13);
            this.lblLocation.TabIndex = 19;
            this.lblLocation.Text = "Location";
            // 
            // gvLicense
            // 
            this.gvLicense.AllowUserToAddRows = false;
            this.gvLicense.AllowUserToDeleteRows = false;
            this.gvLicense.AllowUserToResizeRows = false;
            this.gvLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLicense.Location = new System.Drawing.Point(66, 59);
            this.gvLicense.MultiSelect = false;
            this.gvLicense.Name = "gvLicense";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvLicense.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvLicense.RowHeadersWidth = 75;
            this.gvLicense.RowTemplate.ReadOnly = true;
            this.gvLicense.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvLicense.Size = new System.Drawing.Size(547, 70);
            this.gvLicense.TabIndex = 8;
            // 
            // richtxtResults
            // 
            this.richtxtResults.Location = new System.Drawing.Point(65, 165);
            this.richtxtResults.Name = "richtxtResults";
            this.richtxtResults.Size = new System.Drawing.Size(548, 57);
            this.richtxtResults.TabIndex = 18;
            this.richtxtResults.Text = "";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(16, 184);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(43, 13);
            this.lblResult.TabIndex = 17;
            this.lblResult.Text = "Result";
            // 
            // lblPortValue
            // 
            this.lblPortValue.AutoSize = true;
            this.lblPortValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortValue.Location = new System.Drawing.Point(264, 43);
            this.lblPortValue.Name = "lblPortValue";
            this.lblPortValue.Size = new System.Drawing.Size(26, 13);
            this.lblPortValue.TabIndex = 10;
            this.lblPortValue.Text = "Port";
            // 
            // lblDBNameValue
            // 
            this.lblDBNameValue.AutoSize = true;
            this.lblDBNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBNameValue.Location = new System.Drawing.Point(351, 43);
            this.lblDBNameValue.Name = "lblDBNameValue";
            this.lblDBNameValue.Size = new System.Drawing.Size(53, 13);
            this.lblDBNameValue.TabIndex = 12;
            this.lblDBNameValue.Text = "DB Name";
            // 
            // lblDBserverValue
            // 
            this.lblDBserverValue.AutoSize = true;
            this.lblDBserverValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBserverValue.Location = new System.Drawing.Point(171, 43);
            this.lblDBserverValue.Name = "lblDBserverValue";
            this.lblDBserverValue.Size = new System.Drawing.Size(56, 13);
            this.lblDBserverValue.TabIndex = 7;
            this.lblDBserverValue.Text = "DB Server";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Location = new System.Drawing.Point(236, 43);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(30, 13);
            this.lblPort.TabIndex = 9;
            this.lblPort.Text = "Port";
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBranch.Location = new System.Drawing.Point(409, 10);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(47, 13);
            this.lblBranch.TabIndex = 2;
            this.lblBranch.Text = "Branch";
            // 
            // lblDBName
            // 
            this.lblDBName.AutoSize = true;
            this.lblDBName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBName.Location = new System.Drawing.Point(295, 43);
            this.lblDBName.Name = "lblDBName";
            this.lblDBName.Size = new System.Drawing.Size(60, 13);
            this.lblDBName.TabIndex = 11;
            this.lblDBName.Text = "DB Name";
            // 
            // lblVersionValue
            // 
            this.lblVersionValue.AutoSize = true;
            this.lblVersionValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionValue.Location = new System.Drawing.Point(62, 43);
            this.lblVersionValue.Name = "lblVersionValue";
            this.lblVersionValue.Size = new System.Drawing.Size(42, 13);
            this.lblVersionValue.TabIndex = 5;
            this.lblVersionValue.Text = "Version";
            // 
            // lblDBserver
            // 
            this.lblDBserver.AutoSize = true;
            this.lblDBserver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBserver.Location = new System.Drawing.Point(109, 43);
            this.lblDBserver.Name = "lblDBserver";
            this.lblDBserver.Size = new System.Drawing.Size(65, 13);
            this.lblDBserver.TabIndex = 6;
            this.lblDBserver.Text = "DB Server";
            // 
            // lblPathValue
            // 
            this.lblPathValue.AutoSize = true;
            this.lblPathValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPathValue.Location = new System.Drawing.Point(62, 14);
            this.lblPathValue.Name = "lblPathValue";
            this.lblPathValue.Size = new System.Drawing.Size(29, 13);
            this.lblPathValue.TabIndex = 1;
            this.lblPathValue.Text = "Path";
            // 
            // lbllicenseInfo
            // 
            this.lbllicenseInfo.AutoSize = true;
            this.lbllicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllicenseInfo.Location = new System.Drawing.Point(8, 80);
            this.lbllicenseInfo.Name = "lbllicenseInfo";
            this.lbllicenseInfo.Size = new System.Drawing.Size(51, 13);
            this.lbllicenseInfo.TabIndex = 13;
            this.lbllicenseInfo.Text = "License";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(8, 43);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(49, 13);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "Version";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(26, 14);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(33, 13);
            this.lblPath.TabIndex = 0;
            this.lblPath.Text = "Path";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.Location = new System.Drawing.Point(16, 140);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(43, 13);
            this.lblAction.TabIndex = 14;
            this.lblAction.Text = "Action";
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(461, 3);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(152, 24);
            this.cboBranch.TabIndex = 3;
            this.cboBranch.SelectedIndexChanged += new System.EventHandler(this.cboBranch_SelectedIndexChanged);
            // 
            // cboAction
            // 
            this.cboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAction.FormattingEnabled = true;
            this.cboAction.Location = new System.Drawing.Point(65, 135);
            this.cboAction.Name = "cboAction";
            this.cboAction.Size = new System.Drawing.Size(455, 24);
            this.cboAction.TabIndex = 15;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(523, 135);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 23);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "Okay";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblLocalBOName
            // 
            this.lblLocalBOName.AutoSize = true;
            this.lblLocalBOName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalBOName.Location = new System.Drawing.Point(2, 450);
            this.lblLocalBOName.Name = "lblLocalBOName";
            this.lblLocalBOName.Size = new System.Drawing.Size(82, 13);
            this.lblLocalBOName.TabIndex = 5;
            this.lblLocalBOName.Text = "Local BO Name";
            // 
            // gboxDeleteProject
            // 
            this.gboxDeleteProject.Controls.Add(this.lblDeleteProject);
            this.gboxDeleteProject.Controls.Add(this.btnRemoveProject);
            this.gboxDeleteProject.Controls.Add(this.cboProject);
            this.gboxDeleteProject.Location = new System.Drawing.Point(5, 233);
            this.gboxDeleteProject.Name = "gboxDeleteProject";
            this.gboxDeleteProject.Size = new System.Drawing.Size(620, 61);
            this.gboxDeleteProject.TabIndex = 1;
            this.gboxDeleteProject.TabStop = false;
            this.gboxDeleteProject.Text = "Delete Project";
            this.gboxDeleteProject.Visible = false;
            // 
            // lblDeleteProject
            // 
            this.lblDeleteProject.AutoSize = true;
            this.lblDeleteProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeleteProject.Location = new System.Drawing.Point(5, 28);
            this.lblDeleteProject.Name = "lblDeleteProject";
            this.lblDeleteProject.Size = new System.Drawing.Size(47, 13);
            this.lblDeleteProject.TabIndex = 0;
            this.lblDeleteProject.Text = "Project";
            // 
            // btnRemoveProject
            // 
            this.btnRemoveProject.Location = new System.Drawing.Point(532, 24);
            this.btnRemoveProject.Name = "btnRemoveProject";
            this.btnRemoveProject.Size = new System.Drawing.Size(82, 23);
            this.btnRemoveProject.TabIndex = 2;
            this.btnRemoveProject.Text = "Delete Project";
            this.btnRemoveProject.UseVisualStyleBackColor = true;
            this.btnRemoveProject.Click += new System.EventHandler(this.btnRemoveProject_Click);
            // 
            // cboProject
            // 
            this.cboProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProject.FormattingEnabled = true;
            this.cboProject.Location = new System.Drawing.Point(54, 23);
            this.cboProject.Name = "cboProject";
            this.cboProject.Size = new System.Drawing.Size(475, 24);
            this.cboProject.TabIndex = 1;
            // 
            // gboxDeleteFD
            // 
            this.gboxDeleteFD.Controls.Add(this.lblFDProject);
            this.gboxDeleteFD.Controls.Add(this.cbFDProject);
            this.gboxDeleteFD.Controls.Add(this.lblFD);
            this.gboxDeleteFD.Controls.Add(this.btnFDDelete);
            this.gboxDeleteFD.Controls.Add(this.cboFD);
            this.gboxDeleteFD.Location = new System.Drawing.Point(5, 296);
            this.gboxDeleteFD.Name = "gboxDeleteFD";
            this.gboxDeleteFD.Size = new System.Drawing.Size(620, 80);
            this.gboxDeleteFD.TabIndex = 2;
            this.gboxDeleteFD.TabStop = false;
            this.gboxDeleteFD.Text = "Delete FD Account";
            this.gboxDeleteFD.Visible = false;
            // 
            // lblFDProject
            // 
            this.lblFDProject.AutoSize = true;
            this.lblFDProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFDProject.Location = new System.Drawing.Point(5, 24);
            this.lblFDProject.Name = "lblFDProject";
            this.lblFDProject.Size = new System.Drawing.Size(47, 13);
            this.lblFDProject.TabIndex = 0;
            this.lblFDProject.Text = "Project";
            // 
            // cbFDProject
            // 
            this.cbFDProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFDProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFDProject.FormattingEnabled = true;
            this.cbFDProject.Location = new System.Drawing.Point(54, 19);
            this.cbFDProject.Name = "cbFDProject";
            this.cbFDProject.Size = new System.Drawing.Size(560, 24);
            this.cbFDProject.TabIndex = 1;
            this.cbFDProject.SelectedIndexChanged += new System.EventHandler(this.cbFDProject_SelectedIndexChanged);
            // 
            // lblFD
            // 
            this.lblFD.AutoSize = true;
            this.lblFD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFD.Location = new System.Drawing.Point(25, 54);
            this.lblFD.Name = "lblFD";
            this.lblFD.Size = new System.Drawing.Size(23, 13);
            this.lblFD.TabIndex = 2;
            this.lblFD.Text = "FD";
            // 
            // btnFDDelete
            // 
            this.btnFDDelete.Location = new System.Drawing.Point(553, 48);
            this.btnFDDelete.Name = "btnFDDelete";
            this.btnFDDelete.Size = new System.Drawing.Size(61, 23);
            this.btnFDDelete.TabIndex = 4;
            this.btnFDDelete.Text = "Delete FD";
            this.btnFDDelete.UseVisualStyleBackColor = true;
            this.btnFDDelete.Click += new System.EventHandler(this.btnFDDelete_Click);
            // 
            // cboFD
            // 
            this.cboFD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFD.FormattingEnabled = true;
            this.cboFD.Location = new System.Drawing.Point(54, 48);
            this.cboFD.Name = "cboFD";
            this.cboFD.Size = new System.Drawing.Size(496, 24);
            this.cboFD.TabIndex = 3;
            // 
            // gboxConnectionString
            // 
            this.gboxConnectionString.Controls.Add(this.txtConnectionString);
            this.gboxConnectionString.Controls.Add(this.lblConnectionString);
            this.gboxConnectionString.Controls.Add(this.btnConnectionStringUpdate);
            this.gboxConnectionString.Location = new System.Drawing.Point(4, 378);
            this.gboxConnectionString.Name = "gboxConnectionString";
            this.gboxConnectionString.Size = new System.Drawing.Size(620, 61);
            this.gboxConnectionString.TabIndex = 3;
            this.gboxConnectionString.TabStop = false;
            this.gboxConnectionString.Text = "Update Connection String";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectionString.Location = new System.Drawing.Point(77, 20);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(474, 22);
            this.txtConnectionString.TabIndex = 1;
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionString.Location = new System.Drawing.Point(6, 25);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(71, 13);
            this.lblConnectionString.TabIndex = 0;
            this.lblConnectionString.Text = "Connection";
            // 
            // btnConnectionStringUpdate
            // 
            this.btnConnectionStringUpdate.Location = new System.Drawing.Point(554, 19);
            this.btnConnectionStringUpdate.Name = "btnConnectionStringUpdate";
            this.btnConnectionStringUpdate.Size = new System.Drawing.Size(61, 23);
            this.btnConnectionStringUpdate.TabIndex = 2;
            this.btnConnectionStringUpdate.Text = "Update";
            this.btnConnectionStringUpdate.UseVisualStyleBackColor = true;
            this.btnConnectionStringUpdate.Click += new System.EventHandler(this.btnConnectionStringUpdate_Click);
            // 
            // gboxAcmeerpService
            // 
            this.gboxAcmeerpService.Controls.Add(this.btnINIOpen);
            this.gboxAcmeerpService.Controls.Add(this.lblServiceINIValue);
            this.gboxAcmeerpService.Controls.Add(this.lblServiceINI);
            this.gboxAcmeerpService.Controls.Add(this.lblMySQLInstalledPathValue);
            this.gboxAcmeerpService.Controls.Add(this.btnCreateService);
            this.gboxAcmeerpService.Controls.Add(this.btnRemoveService);
            this.gboxAcmeerpService.Controls.Add(this.btnServiceAction);
            this.gboxAcmeerpService.Controls.Add(this.lblServiceStatusValue);
            this.gboxAcmeerpService.Controls.Add(this.lblServiceStatus);
            this.gboxAcmeerpService.Controls.Add(this.lblServicePortValue);
            this.gboxAcmeerpService.Controls.Add(this.lblMySQLInstalledPath);
            this.gboxAcmeerpService.Controls.Add(this.lblServiceInstalled);
            this.gboxAcmeerpService.Controls.Add(this.lblServicePort);
            this.gboxAcmeerpService.Controls.Add(this.lblServiceInstalledValue);
            this.gboxAcmeerpService.Location = new System.Drawing.Point(634, 4);
            this.gboxAcmeerpService.Name = "gboxAcmeerpService";
            this.gboxAcmeerpService.Size = new System.Drawing.Size(620, 110);
            this.gboxAcmeerpService.TabIndex = 4;
            this.gboxAcmeerpService.TabStop = false;
            this.gboxAcmeerpService.Text = "Acmeerp MySQL Service Info";
            this.gboxAcmeerpService.Visible = false;
            // 
            // btnINIOpen
            // 
            this.btnINIOpen.Location = new System.Drawing.Point(197, 46);
            this.btnINIOpen.Name = "btnINIOpen";
            this.btnINIOpen.Size = new System.Drawing.Size(75, 23);
            this.btnINIOpen.TabIndex = 9;
            this.btnINIOpen.Text = "Open INI File";
            this.btnINIOpen.UseVisualStyleBackColor = true;
            this.btnINIOpen.Click += new System.EventHandler(this.btnINIOpen_Click);
            // 
            // lblServiceINIValue
            // 
            this.lblServiceINIValue.AutoSize = true;
            this.lblServiceINIValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceINIValue.Location = new System.Drawing.Point(114, 52);
            this.lblServiceINIValue.Name = "lblServiceINIValue";
            this.lblServiceINIValue.Size = new System.Drawing.Size(78, 13);
            this.lblServiceINIValue.TabIndex = 8;
            this.lblServiceINIValue.Text = "C:\\AcMEERP\\";
            // 
            // lblServiceINI
            // 
            this.lblServiceINI.AutoSize = true;
            this.lblServiceINI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceINI.Location = new System.Drawing.Point(4, 52);
            this.lblServiceINI.Name = "lblServiceINI";
            this.lblServiceINI.Size = new System.Drawing.Size(113, 13);
            this.lblServiceINI.TabIndex = 7;
            this.lblServiceINI.Text = "Service INI Path : ";
            // 
            // lblMySQLInstalledPathValue
            // 
            this.lblMySQLInstalledPathValue.AutoSize = true;
            this.lblMySQLInstalledPathValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMySQLInstalledPathValue.Location = new System.Drawing.Point(144, 83);
            this.lblMySQLInstalledPathValue.Name = "lblMySQLInstalledPathValue";
            this.lblMySQLInstalledPathValue.Size = new System.Drawing.Size(80, 13);
            this.lblMySQLInstalledPathValue.TabIndex = 12;
            this.lblMySQLInstalledPathValue.Text = "C:\\MySQL 5.6\\";
            // 
            // btnCreateService
            // 
            this.btnCreateService.Location = new System.Drawing.Point(442, 52);
            this.btnCreateService.Name = "btnCreateService";
            this.btnCreateService.Size = new System.Drawing.Size(128, 23);
            this.btnCreateService.TabIndex = 10;
            this.btnCreateService.Text = "Create Service";
            this.btnCreateService.UseVisualStyleBackColor = true;
            this.btnCreateService.Click += new System.EventHandler(this.btnCreateService_Click);
            // 
            // btnRemoveService
            // 
            this.btnRemoveService.Location = new System.Drawing.Point(442, 81);
            this.btnRemoveService.Name = "btnRemoveService";
            this.btnRemoveService.Size = new System.Drawing.Size(128, 23);
            this.btnRemoveService.TabIndex = 13;
            this.btnRemoveService.Text = "Remove Service";
            this.btnRemoveService.UseVisualStyleBackColor = true;
            this.btnRemoveService.Click += new System.EventHandler(this.btnRemoveService_Click);
            // 
            // btnServiceAction
            // 
            this.btnServiceAction.Location = new System.Drawing.Point(442, 19);
            this.btnServiceAction.Name = "btnServiceAction";
            this.btnServiceAction.Size = new System.Drawing.Size(128, 23);
            this.btnServiceAction.TabIndex = 6;
            this.btnServiceAction.Text = "Start";
            this.btnServiceAction.UseVisualStyleBackColor = true;
            this.btnServiceAction.Click += new System.EventHandler(this.btnServiceAction_Click);
            // 
            // lblServiceStatusValue
            // 
            this.lblServiceStatusValue.AutoSize = true;
            this.lblServiceStatusValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceStatusValue.Location = new System.Drawing.Point(378, 23);
            this.lblServiceStatusValue.Name = "lblServiceStatusValue";
            this.lblServiceStatusValue.Size = new System.Drawing.Size(47, 13);
            this.lblServiceStatusValue.TabIndex = 5;
            this.lblServiceStatusValue.Text = "Running";
            // 
            // lblServiceStatus
            // 
            this.lblServiceStatus.AutoSize = true;
            this.lblServiceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceStatus.Location = new System.Drawing.Point(329, 23);
            this.lblServiceStatus.Name = "lblServiceStatus";
            this.lblServiceStatus.Size = new System.Drawing.Size(55, 13);
            this.lblServiceStatus.TabIndex = 4;
            this.lblServiceStatus.Text = "Status : ";
            // 
            // lblServicePortValue
            // 
            this.lblServicePortValue.AutoSize = true;
            this.lblServicePortValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServicePortValue.Location = new System.Drawing.Point(232, 23);
            this.lblServicePortValue.Name = "lblServicePortValue";
            this.lblServicePortValue.Size = new System.Drawing.Size(31, 13);
            this.lblServicePortValue.TabIndex = 3;
            this.lblServicePortValue.Text = "3306";
            // 
            // lblMySQLInstalledPath
            // 
            this.lblMySQLInstalledPath.AutoSize = true;
            this.lblMySQLInstalledPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMySQLInstalledPath.Location = new System.Drawing.Point(4, 83);
            this.lblMySQLInstalledPath.Name = "lblMySQLInstalledPath";
            this.lblMySQLInstalledPath.Size = new System.Drawing.Size(141, 13);
            this.lblMySQLInstalledPath.TabIndex = 11;
            this.lblMySQLInstalledPath.Text = "MySQL Installed Path : ";
            // 
            // lblServiceInstalled
            // 
            this.lblServiceInstalled.AutoSize = true;
            this.lblServiceInstalled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceInstalled.Location = new System.Drawing.Point(4, 23);
            this.lblServiceInstalled.Name = "lblServiceInstalled";
            this.lblServiceInstalled.Size = new System.Drawing.Size(55, 13);
            this.lblServiceInstalled.TabIndex = 0;
            this.lblServiceInstalled.Text = "Installed";
            // 
            // lblServicePort
            // 
            this.lblServicePort.AutoSize = true;
            this.lblServicePort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServicePort.Location = new System.Drawing.Point(201, 23);
            this.lblServicePort.Name = "lblServicePort";
            this.lblServicePort.Size = new System.Drawing.Size(30, 13);
            this.lblServicePort.TabIndex = 2;
            this.lblServicePort.Text = "Port";
            // 
            // lblServiceInstalledValue
            // 
            this.lblServiceInstalledValue.AutoSize = true;
            this.lblServiceInstalledValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceInstalledValue.Location = new System.Drawing.Point(58, 23);
            this.lblServiceInstalledValue.Name = "lblServiceInstalledValue";
            this.lblServiceInstalledValue.Size = new System.Drawing.Size(67, 13);
            this.lblServiceInstalledValue.TabIndex = 1;
            this.lblServiceInstalledValue.Text = "No, Running";
            // 
            // gboxDeleteFDAll
            // 
            this.gboxDeleteFDAll.Controls.Add(this.lblAllFDConfirmationMessge);
            this.gboxDeleteFDAll.Controls.Add(this.btnDeleteAllFDs);
            this.gboxDeleteFDAll.Location = new System.Drawing.Point(634, 122);
            this.gboxDeleteFDAll.Name = "gboxDeleteFDAll";
            this.gboxDeleteFDAll.Size = new System.Drawing.Size(620, 53);
            this.gboxDeleteFDAll.TabIndex = 7;
            this.gboxDeleteFDAll.TabStop = false;
            this.gboxDeleteFDAll.Text = "Delete all FDs in Acmeerp";
            this.gboxDeleteFDAll.Visible = false;
            // 
            // lblAllFDConfirmationMessge
            // 
            this.lblAllFDConfirmationMessge.AutoSize = true;
            this.lblAllFDConfirmationMessge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllFDConfirmationMessge.Location = new System.Drawing.Point(6, 25);
            this.lblAllFDConfirmationMessge.Name = "lblAllFDConfirmationMessge";
            this.lblAllFDConfirmationMessge.Size = new System.Drawing.Size(350, 13);
            this.lblAllFDConfirmationMessge.TabIndex = 2;
            this.lblAllFDConfirmationMessge.Text = "Are you sure to delete all FDs which includes opening also ?";
            // 
            // btnDeleteAllFDs
            // 
            this.btnDeleteAllFDs.Location = new System.Drawing.Point(516, 18);
            this.btnDeleteAllFDs.Name = "btnDeleteAllFDs";
            this.btnDeleteAllFDs.Size = new System.Drawing.Size(98, 23);
            this.btnDeleteAllFDs.TabIndex = 4;
            this.btnDeleteAllFDs.Text = "Delete all FDs";
            this.btnDeleteAllFDs.UseVisualStyleBackColor = true;
            this.btnDeleteAllFDs.Click += new System.EventHandler(this.btnDeleteAllFDs_Click);
            // 
            // lblRefreshMessage
            // 
            this.lblRefreshMessage.AutoSize = true;
            this.lblRefreshMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefreshMessage.ForeColor = System.Drawing.Color.Red;
            this.lblRefreshMessage.Location = new System.Drawing.Point(113, 455);
            this.lblRefreshMessage.Name = "lblRefreshMessage";
            this.lblRefreshMessage.Size = new System.Drawing.Size(349, 13);
            this.lblRefreshMessage.TabIndex = 20;
            this.lblRefreshMessage.Text = "After using this Tool, Refresh \"Ledger Balance\" in Acme.erp";
            // 
            // gboxUpdateLocation
            // 
            this.gboxUpdateLocation.Controls.Add(this.lblLicenseLocations);
            this.gboxUpdateLocation.Controls.Add(this.btnUpdateLocation);
            this.gboxUpdateLocation.Controls.Add(this.cboLocations);
            this.gboxUpdateLocation.Location = new System.Drawing.Point(634, 181);
            this.gboxUpdateLocation.Name = "gboxUpdateLocation";
            this.gboxUpdateLocation.Size = new System.Drawing.Size(620, 61);
            this.gboxUpdateLocation.TabIndex = 21;
            this.gboxUpdateLocation.TabStop = false;
            this.gboxUpdateLocation.Text = "Update Location in Acmeerp Setting";
            this.gboxUpdateLocation.Visible = false;
            // 
            // lblLicenseLocations
            // 
            this.lblLicenseLocations.AutoSize = true;
            this.lblLicenseLocations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseLocations.Location = new System.Drawing.Point(5, 28);
            this.lblLicenseLocations.Name = "lblLicenseLocations";
            this.lblLicenseLocations.Size = new System.Drawing.Size(138, 13);
            this.lblLicenseLocations.TabIndex = 0;
            this.lblLicenseLocations.Text = "Locations from License";
            // 
            // btnUpdateLocation
            // 
            this.btnUpdateLocation.Location = new System.Drawing.Point(473, 24);
            this.btnUpdateLocation.Name = "btnUpdateLocation";
            this.btnUpdateLocation.Size = new System.Drawing.Size(141, 23);
            this.btnUpdateLocation.TabIndex = 2;
            this.btnUpdateLocation.Text = "Update Location in Setting";
            this.btnUpdateLocation.UseVisualStyleBackColor = true;
            this.btnUpdateLocation.Click += new System.EventHandler(this.btnUpdateLocation_Click);
            // 
            // cboLocations
            // 
            this.cboLocations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocations.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLocations.FormattingEnabled = true;
            this.cboLocations.Location = new System.Drawing.Point(147, 23);
            this.cboLocations.Name = "cboLocations";
            this.cboLocations.Size = new System.Drawing.Size(320, 24);
            this.cboLocations.TabIndex = 1;
            // 
            // gboxResetDBVouchers
            // 
            this.gboxResetDBVouchers.Controls.Add(this.chkClearOP);
            this.gboxResetDBVouchers.Controls.Add(this.lblNote);
            this.gboxResetDBVouchers.Controls.Add(this.dateBooksBegin);
            this.gboxResetDBVouchers.Controls.Add(this.label1);
            this.gboxResetDBVouchers.Controls.Add(this.dateTo);
            this.gboxResetDBVouchers.Controls.Add(this.lblDateTo);
            this.gboxResetDBVouchers.Controls.Add(this.dateFrom);
            this.gboxResetDBVouchers.Controls.Add(this.lblDateFrom);
            this.gboxResetDBVouchers.Controls.Add(this.btnResetDBVoucher);
            this.gboxResetDBVouchers.Location = new System.Drawing.Point(634, 256);
            this.gboxResetDBVouchers.Name = "gboxResetDBVouchers";
            this.gboxResetDBVouchers.Size = new System.Drawing.Size(620, 95);
            this.gboxResetDBVouchers.TabIndex = 22;
            this.gboxResetDBVouchers.TabStop = false;
            this.gboxResetDBVouchers.Text = "Clear and Reset Vouchers";
            this.gboxResetDBVouchers.Visible = false;
            // 
            // chkClearOP
            // 
            this.chkClearOP.AutoSize = true;
            this.chkClearOP.Location = new System.Drawing.Point(332, 60);
            this.chkClearOP.Name = "chkClearOP";
            this.chkClearOP.Size = new System.Drawing.Size(135, 17);
            this.chkClearOP.TabIndex = 10;
            this.chkClearOP.Text = "Clear Opening Balance";
            this.chkClearOP.UseVisualStyleBackColor = true;
            // 
            // lblNote
            // 
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(6, 48);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(320, 44);
            this.lblNote.TabIndex = 9;
            this.lblNote.Text = "* Clear and reset all Vouchers, Above FY will be created. All Masters will remain" +
                " same ";
            // 
            // dateBooksBegin
            // 
            this.dateBooksBegin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateBooksBegin.Location = new System.Drawing.Point(515, 22);
            this.dateBooksBegin.Name = "dateBooksBegin";
            this.dateBooksBegin.Size = new System.Drawing.Size(99, 20);
            this.dateBooksBegin.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(376, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Books Beginning From";
            // 
            // dateTo
            // 
            this.dateTo.Enabled = false;
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTo.Location = new System.Drawing.Point(235, 22);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(99, 20);
            this.dateTo.TabIndex = 6;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTo.Location = new System.Drawing.Point(180, 25);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(52, 13);
            this.lblDateTo.TabIndex = 5;
            this.lblDateTo.Text = "Year To";
            // 
            // dateFrom
            // 
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFrom.Location = new System.Drawing.Point(70, 22);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(99, 20);
            this.dateFrom.TabIndex = 4;
            this.dateFrom.ValueChanged += new System.EventHandler(this.dateFrom_ValueChanged);
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFrom.Location = new System.Drawing.Point(5, 25);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(64, 13);
            this.lblDateFrom.TabIndex = 3;
            this.lblDateFrom.Text = "Year From";
            // 
            // btnResetDBVoucher
            // 
            this.btnResetDBVoucher.Location = new System.Drawing.Point(473, 56);
            this.btnResetDBVoucher.Name = "btnResetDBVoucher";
            this.btnResetDBVoucher.Size = new System.Drawing.Size(141, 23);
            this.btnResetDBVoucher.TabIndex = 2;
            this.btnResetDBVoucher.Text = "Clear and Reset Vouchers";
            this.btnResetDBVoucher.UseVisualStyleBackColor = true;
            this.btnResetDBVoucher.Click += new System.EventHandler(this.btnResetDBVoucher_Click);
            // 
            // gboxRestoreDatabase
            // 
            this.gboxRestoreDatabase.Controls.Add(this.lblNewDB);
            this.gboxRestoreDatabase.Controls.Add(this.txtNewSchema);
            this.gboxRestoreDatabase.Controls.Add(this.btnFileBrowse);
            this.gboxRestoreDatabase.Controls.Add(this.txtDBbackupPath);
            this.gboxRestoreDatabase.Controls.Add(this.lblDBBackupPath);
            this.gboxRestoreDatabase.Controls.Add(this.btnDBRestore);
            this.gboxRestoreDatabase.Location = new System.Drawing.Point(634, 359);
            this.gboxRestoreDatabase.Name = "gboxRestoreDatabase";
            this.gboxRestoreDatabase.Size = new System.Drawing.Size(620, 80);
            this.gboxRestoreDatabase.TabIndex = 23;
            this.gboxRestoreDatabase.TabStop = false;
            this.gboxRestoreDatabase.Text = "Restore Selected Database";
            this.gboxRestoreDatabase.Visible = false;
            // 
            // lblNewDB
            // 
            this.lblNewDB.AutoSize = true;
            this.lblNewDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewDB.Location = new System.Drawing.Point(16, 50);
            this.lblNewDB.Name = "lblNewDB";
            this.lblNewDB.Size = new System.Drawing.Size(90, 13);
            this.lblNewDB.TabIndex = 6;
            this.lblNewDB.Text = "New Database";
            // 
            // txtNewSchema
            // 
            this.txtNewSchema.Location = new System.Drawing.Point(112, 47);
            this.txtNewSchema.MaxLength = 25;
            this.txtNewSchema.Name = "txtNewSchema";
            this.txtNewSchema.Size = new System.Drawing.Size(178, 20);
            this.txtNewSchema.TabIndex = 5;
            // 
            // btnFileBrowse
            // 
            this.btnFileBrowse.Location = new System.Drawing.Point(579, 18);
            this.btnFileBrowse.Name = "btnFileBrowse";
            this.btnFileBrowse.Size = new System.Drawing.Size(35, 21);
            this.btnFileBrowse.TabIndex = 4;
            this.btnFileBrowse.Text = "...";
            this.btnFileBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFileBrowse.UseVisualStyleBackColor = true;
            this.btnFileBrowse.Click += new System.EventHandler(this.btnFileBrowse_Click);
            // 
            // txtDBbackupPath
            // 
            this.txtDBbackupPath.Enabled = false;
            this.txtDBbackupPath.Location = new System.Drawing.Point(112, 19);
            this.txtDBbackupPath.Name = "txtDBbackupPath";
            this.txtDBbackupPath.Size = new System.Drawing.Size(461, 20);
            this.txtDBbackupPath.TabIndex = 3;
            // 
            // lblDBBackupPath
            // 
            this.lblDBBackupPath.AutoSize = true;
            this.lblDBBackupPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBBackupPath.Location = new System.Drawing.Point(5, 22);
            this.lblDBBackupPath.Name = "lblDBBackupPath";
            this.lblDBBackupPath.Size = new System.Drawing.Size(101, 13);
            this.lblDBBackupPath.TabIndex = 0;
            this.lblDBBackupPath.Text = "DB Backup Path";
            // 
            // btnDBRestore
            // 
            this.btnDBRestore.Location = new System.Drawing.Point(473, 49);
            this.btnDBRestore.Name = "btnDBRestore";
            this.btnDBRestore.Size = new System.Drawing.Size(141, 23);
            this.btnDBRestore.TabIndex = 2;
            this.btnDBRestore.Text = "Restore";
            this.btnDBRestore.UseVisualStyleBackColor = true;
            this.btnDBRestore.Click += new System.EventHandler(this.btnDBRestore_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1260, 472);
            this.Controls.Add(this.gboxRestoreDatabase);
            this.Controls.Add(this.gboxResetDBVouchers);
            this.Controls.Add(this.gboxUpdateLocation);
            this.Controls.Add(this.lblRefreshMessage);
            this.Controls.Add(this.gboxDeleteFDAll);
            this.Controls.Add(this.gboxAcmeerpService);
            this.Controls.Add(this.gboxConnectionString);
            this.Controls.Add(this.gboxDeleteFD);
            this.Controls.Add(this.gboxDeleteProject);
            this.Controls.Add(this.lblLocalBOName);
            this.Controls.Add(this.pnlAcmeerp);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acme.erp Supporting Tool";
            this.pnlAcmeerp.ResumeLayout(false);
            this.pnlAcmeerp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLicense)).EndInit();
            this.gboxDeleteProject.ResumeLayout(false);
            this.gboxDeleteProject.PerformLayout();
            this.gboxDeleteFD.ResumeLayout(false);
            this.gboxDeleteFD.PerformLayout();
            this.gboxConnectionString.ResumeLayout(false);
            this.gboxConnectionString.PerformLayout();
            this.gboxAcmeerpService.ResumeLayout(false);
            this.gboxAcmeerpService.PerformLayout();
            this.gboxDeleteFDAll.ResumeLayout(false);
            this.gboxDeleteFDAll.PerformLayout();
            this.gboxUpdateLocation.ResumeLayout(false);
            this.gboxUpdateLocation.PerformLayout();
            this.gboxResetDBVouchers.ResumeLayout(false);
            this.gboxResetDBVouchers.PerformLayout();
            this.gboxRestoreDatabase.ResumeLayout(false);
            this.gboxRestoreDatabase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlAcmeerp;
        private System.Windows.Forms.Label lblVersionValue;
        private System.Windows.Forms.Label lblPathValue;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.ComboBox cboAction;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RichTextBox richtxtResults;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblDBserverValue;
        private System.Windows.Forms.Label lblDBserver;
        private System.Windows.Forms.Label lblDBNameValue;
        private System.Windows.Forms.Label lblDBName;
        private System.Windows.Forms.Label lblPortValue;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.DataGridView gvLicense;
        private System.Windows.Forms.Label lbllicenseInfo;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cboBranch;
        private System.Windows.Forms.Label lblLocalBOName;
        private System.Windows.Forms.GroupBox gboxDeleteProject;
        private System.Windows.Forms.Label lblDeleteProject;
        private System.Windows.Forms.Button btnRemoveProject;
        private System.Windows.Forms.ComboBox cboProject;
        private System.Windows.Forms.GroupBox gboxDeleteFD;
        private System.Windows.Forms.Label lblFDProject;
        private System.Windows.Forms.ComboBox cbFDProject;
        private System.Windows.Forms.Label lblFD;
        private System.Windows.Forms.Button btnFDDelete;
        private System.Windows.Forms.ComboBox cboFD;
        private System.Windows.Forms.GroupBox gboxConnectionString;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.Button btnConnectionStringUpdate;
        private System.Windows.Forms.GroupBox gboxAcmeerpService;
        private System.Windows.Forms.Button btnINIOpen;
        private System.Windows.Forms.Label lblServiceINIValue;
        private System.Windows.Forms.Label lblServiceINI;
        private System.Windows.Forms.Label lblMySQLInstalledPathValue;
        private System.Windows.Forms.Button btnServiceAction;
        private System.Windows.Forms.Label lblServiceStatusValue;
        private System.Windows.Forms.Label lblServiceStatus;
        private System.Windows.Forms.Label lblServicePortValue;
        private System.Windows.Forms.Label lblMySQLInstalledPath;
        private System.Windows.Forms.Label lblServiceInstalled;
        private System.Windows.Forms.Label lblServicePort;
        private System.Windows.Forms.Label lblServiceInstalledValue;
        private System.Windows.Forms.Button btnRemoveService;
        private System.Windows.Forms.Button btnCreateService;
        private System.Windows.Forms.GroupBox gboxDeleteFDAll;
        private System.Windows.Forms.Label lblAllFDConfirmationMessge;
        private System.Windows.Forms.Button btnDeleteAllFDs;
        private System.Windows.Forms.Label lblRefreshMessage;
        private System.Windows.Forms.GroupBox gboxUpdateLocation;
        private System.Windows.Forms.Label lblLicenseLocations;
        private System.Windows.Forms.Button btnUpdateLocation;
        private System.Windows.Forms.ComboBox cboLocations;
        private System.Windows.Forms.Label lblLocationValue;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.GroupBox gboxResetDBVouchers;
        private System.Windows.Forms.Button btnResetDBVoucher;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateBooksBegin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.CheckBox chkClearOP;
        private System.Windows.Forms.GroupBox gboxRestoreDatabase;
        private System.Windows.Forms.Label lblDBBackupPath;
        private System.Windows.Forms.Button btnDBRestore;
        private System.Windows.Forms.OpenFileDialog openFileDBBackup;
        private System.Windows.Forms.Button btnFileBrowse;
        private System.Windows.Forms.TextBox txtDBbackupPath;
        private System.Windows.Forms.TextBox txtNewSchema;
        private System.Windows.Forms.Label lblNewDB;
    }
}

