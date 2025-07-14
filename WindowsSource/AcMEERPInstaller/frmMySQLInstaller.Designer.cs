namespace AcMEERPInstaller
{
    partial class frmMySQLInstaller
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
            this.folderBrowserMySQLPath = new System.Windows.Forms.FolderBrowserDialog();
            this.grpbxSummary = new System.Windows.Forms.GroupBox();
            this.lblMySQLInstallPath = new System.Windows.Forms.Label();
            this.btnServiceStatus = new System.Windows.Forms.Button();
            this.lblMySQLACPERPInstallStatus = new System.Windows.Forms.Label();
            this.lblMySQLInstallStatus = new System.Windows.Forms.Label();
            this.grpLicense = new System.Windows.Forms.GroupBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBrachNameValue = new System.Windows.Forms.Label();
            this.lblBarachCodeValue = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblLicenceKey = new System.Windows.Forms.Label();
            this.txtLicenceKey = new System.Windows.Forms.TextBox();
            this.BtnBrowse = new System.Windows.Forms.Button();
            this.grpDBInfo = new System.Windows.Forms.GroupBox();
            this.optServer = new System.Windows.Forms.RadioButton();
            this.optClient = new System.Windows.Forms.RadioButton();
            this.lblDataPath = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtDBPath = new System.Windows.Forms.TextBox();
            this.txtHostName = new System.Windows.Forms.TextBox();
            this.lblInstallationMode = new System.Windows.Forms.Label();
            this.lblDBServer = new System.Windows.Forms.Label();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnDBpath = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSummary = new System.Windows.Forms.Button();
            this.btnInstalMySQL = new System.Windows.Forms.Button();
            this.openLicenceKey = new System.Windows.Forms.OpenFileDialog();
            this.grpbxSummary.SuspendLayout();
            this.grpLicense.SuspendLayout();
            this.grpDBInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbxSummary
            // 
            this.grpbxSummary.Controls.Add(this.lblMySQLInstallPath);
            this.grpbxSummary.Controls.Add(this.btnServiceStatus);
            this.grpbxSummary.Controls.Add(this.lblMySQLACPERPInstallStatus);
            this.grpbxSummary.Controls.Add(this.lblMySQLInstallStatus);
            this.grpbxSummary.Location = new System.Drawing.Point(5, 266);
            this.grpbxSummary.Name = "grpbxSummary";
            this.grpbxSummary.Size = new System.Drawing.Size(425, 91);
            this.grpbxSummary.TabIndex = 2;
            this.grpbxSummary.TabStop = false;
            this.grpbxSummary.Text = "Summary";
            this.grpbxSummary.Visible = false;
            // 
            // lblMySQLInstallPath
            // 
            this.lblMySQLInstallPath.AutoSize = true;
            this.lblMySQLInstallPath.Location = new System.Drawing.Point(4, 19);
            this.lblMySQLInstallPath.Name = "lblMySQLInstallPath";
            this.lblMySQLInstallPath.Size = new System.Drawing.Size(88, 13);
            this.lblMySQLInstallPath.TabIndex = 0;
            this.lblMySQLInstallPath.Text = "Install MySQL To";
            // 
            // btnServiceStatus
            // 
            this.btnServiceStatus.Location = new System.Drawing.Point(228, 59);
            this.btnServiceStatus.Name = "btnServiceStatus";
            this.btnServiceStatus.Size = new System.Drawing.Size(75, 23);
            this.btnServiceStatus.TabIndex = 3;
            this.btnServiceStatus.Text = "Start";
            this.btnServiceStatus.UseVisualStyleBackColor = true;
            this.btnServiceStatus.Click += new System.EventHandler(this.btnServiceStatus_Click);
            // 
            // lblMySQLACPERPInstallStatus
            // 
            this.lblMySQLACPERPInstallStatus.AutoSize = true;
            this.lblMySQLACPERPInstallStatus.Location = new System.Drawing.Point(4, 64);
            this.lblMySQLACPERPInstallStatus.Name = "lblMySQLACPERPInstallStatus";
            this.lblMySQLACPERPInstallStatus.Size = new System.Drawing.Size(169, 13);
            this.lblMySQLACPERPInstallStatus.TabIndex = 2;
            this.lblMySQLACPERPInstallStatus.Text = "MySQLACPERP Service Installed ";
            // 
            // lblMySQLInstallStatus
            // 
            this.lblMySQLInstallStatus.AutoSize = true;
            this.lblMySQLInstallStatus.Location = new System.Drawing.Point(4, 42);
            this.lblMySQLInstallStatus.Name = "lblMySQLInstallStatus";
            this.lblMySQLInstallStatus.Size = new System.Drawing.Size(105, 13);
            this.lblMySQLInstallStatus.TabIndex = 1;
            this.lblMySQLInstallStatus.Text = "MySQL Install Status";
            // 
            // grpLicense
            // 
            this.grpLicense.Controls.Add(this.lblBranch);
            this.grpLicense.Controls.Add(this.label1);
            this.grpLicense.Controls.Add(this.lblBrachNameValue);
            this.grpLicense.Controls.Add(this.lblBarachCodeValue);
            this.grpLicense.Controls.Add(this.btnUpdate);
            this.grpLicense.Controls.Add(this.lblLicenceKey);
            this.grpLicense.Controls.Add(this.txtLicenceKey);
            this.grpLicense.Controls.Add(this.BtnBrowse);
            this.grpLicense.Location = new System.Drawing.Point(5, 0);
            this.grpLicense.Name = "grpLicense";
            this.grpLicense.Size = new System.Drawing.Size(425, 126);
            this.grpLicense.TabIndex = 0;
            this.grpLicense.TabStop = false;
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBranch.Location = new System.Drawing.Point(5, 56);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(69, 13);
            this.lblBranch.TabIndex = 3;
            this.lblBranch.Text = "Branch Code";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Branch Name";
            // 
            // lblBrachNameValue
            // 
            this.lblBrachNameValue.AutoSize = true;
            this.lblBrachNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrachNameValue.Location = new System.Drawing.Point(81, 80);
            this.lblBrachNameValue.Name = "lblBrachNameValue";
            this.lblBrachNameValue.Size = new System.Drawing.Size(191, 13);
            this.lblBrachNameValue.TabIndex = 6;
            this.lblBrachNameValue.Text = "Don Bosco Center. Yellaigir Hills";
            // 
            // lblBarachCodeValue
            // 
            this.lblBarachCodeValue.AutoSize = true;
            this.lblBarachCodeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarachCodeValue.Location = new System.Drawing.Point(81, 56);
            this.lblBarachCodeValue.Name = "lblBarachCodeValue";
            this.lblBarachCodeValue.Size = new System.Drawing.Size(80, 13);
            this.lblBarachCodeValue.TabIndex = 4;
            this.lblBarachCodeValue.Text = "Branch Code";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(341, 97);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblLicenceKey
            // 
            this.lblLicenceKey.AutoSize = true;
            this.lblLicenceKey.Location = new System.Drawing.Point(7, 24);
            this.lblLicenceKey.Name = "lblLicenceKey";
            this.lblLicenceKey.Size = new System.Drawing.Size(65, 13);
            this.lblLicenceKey.TabIndex = 0;
            this.lblLicenceKey.Text = "License Key";
            // 
            // txtLicenceKey
            // 
            this.txtLicenceKey.Location = new System.Drawing.Point(84, 24);
            this.txtLicenceKey.Name = "txtLicenceKey";
            this.txtLicenceKey.ReadOnly = true;
            this.txtLicenceKey.Size = new System.Drawing.Size(259, 20);
            this.txtLicenceKey.TabIndex = 1;
            // 
            // BtnBrowse
            // 
            this.BtnBrowse.Location = new System.Drawing.Point(349, 24);
            this.BtnBrowse.Name = "BtnBrowse";
            this.BtnBrowse.Size = new System.Drawing.Size(68, 23);
            this.BtnBrowse.TabIndex = 2;
            this.BtnBrowse.Text = "Browse";
            this.BtnBrowse.UseVisualStyleBackColor = true;
            this.BtnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // grpDBInfo
            // 
            this.grpDBInfo.Controls.Add(this.optServer);
            this.grpDBInfo.Controls.Add(this.optClient);
            this.grpDBInfo.Controls.Add(this.lblDataPath);
            this.grpDBInfo.Controls.Add(this.txtPort);
            this.grpDBInfo.Controls.Add(this.lblPort);
            this.grpDBInfo.Controls.Add(this.txtDBPath);
            this.grpDBInfo.Controls.Add(this.txtHostName);
            this.grpDBInfo.Controls.Add(this.lblInstallationMode);
            this.grpDBInfo.Controls.Add(this.lblDBServer);
            this.grpDBInfo.Controls.Add(this.btnTestConnection);
            this.grpDBInfo.Controls.Add(this.btnDBpath);
            this.grpDBInfo.Controls.Add(this.btnClose);
            this.grpDBInfo.Controls.Add(this.btnSummary);
            this.grpDBInfo.Controls.Add(this.btnInstalMySQL);
            this.grpDBInfo.Location = new System.Drawing.Point(5, 128);
            this.grpDBInfo.Name = "grpDBInfo";
            this.grpDBInfo.Size = new System.Drawing.Size(425, 136);
            this.grpDBInfo.TabIndex = 1;
            this.grpDBInfo.TabStop = false;
            // 
            // optServer
            // 
            this.optServer.AutoSize = true;
            this.optServer.Checked = true;
            this.optServer.Location = new System.Drawing.Point(98, 14);
            this.optServer.Name = "optServer";
            this.optServer.Size = new System.Drawing.Size(56, 17);
            this.optServer.TabIndex = 1;
            this.optServer.TabStop = true;
            this.optServer.Text = "Server";
            this.optServer.UseVisualStyleBackColor = true;
            this.optServer.Click += new System.EventHandler(this.optServer_CheckedChanged);
            // 
            // optClient
            // 
            this.optClient.AutoSize = true;
            this.optClient.Location = new System.Drawing.Point(202, 14);
            this.optClient.Name = "optClient";
            this.optClient.Size = new System.Drawing.Size(51, 17);
            this.optClient.TabIndex = 2;
            this.optClient.Text = "Client";
            this.optClient.UseVisualStyleBackColor = true;
            this.optClient.Click += new System.EventHandler(this.optClient_CheckedChanged);
            // 
            // lblDataPath
            // 
            this.lblDataPath.AutoSize = true;
            this.lblDataPath.Location = new System.Drawing.Point(37, 66);
            this.lblDataPath.Name = "lblDataPath";
            this.lblDataPath.Size = new System.Drawing.Size(55, 13);
            this.lblDataPath.TabIndex = 7;
            this.lblDataPath.Text = "Data Path";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(301, 37);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(66, 20);
            this.txtPort.TabIndex = 6;
            this.txtPort.Text = "3320";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(269, 40);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "Port";
            // 
            // txtDBPath
            // 
            this.txtDBPath.Location = new System.Drawing.Point(98, 63);
            this.txtDBPath.Name = "txtDBPath";
            this.txtDBPath.Size = new System.Drawing.Size(269, 20);
            this.txtDBPath.TabIndex = 8;
            // 
            // txtHostName
            // 
            this.txtHostName.Location = new System.Drawing.Point(98, 37);
            this.txtHostName.Name = "txtHostName";
            this.txtHostName.Size = new System.Drawing.Size(158, 20);
            this.txtHostName.TabIndex = 4;
            this.txtHostName.Text = "localhost";
            // 
            // lblInstallationMode
            // 
            this.lblInstallationMode.AutoSize = true;
            this.lblInstallationMode.Location = new System.Drawing.Point(5, 15);
            this.lblInstallationMode.Name = "lblInstallationMode";
            this.lblInstallationMode.Size = new System.Drawing.Size(87, 13);
            this.lblInstallationMode.TabIndex = 0;
            this.lblInstallationMode.Text = "Installation Mode";
            // 
            // lblDBServer
            // 
            this.lblDBServer.AutoSize = true;
            this.lblDBServer.Location = new System.Drawing.Point(36, 40);
            this.lblDBServer.Name = "lblDBServer";
            this.lblDBServer.Size = new System.Drawing.Size(56, 13);
            this.lblDBServer.TabIndex = 3;
            this.lblDBServer.Text = "DB Server";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(7, 98);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(134, 23);
            this.btnTestConnection.TabIndex = 12;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnDBpath
            // 
            this.btnDBpath.Location = new System.Drawing.Point(370, 61);
            this.btnDBpath.Name = "btnDBpath";
            this.btnDBpath.Size = new System.Drawing.Size(46, 23);
            this.btnDBpath.TabIndex = 9;
            this.btnDBpath.Text = "...";
            this.btnDBpath.UseVisualStyleBackColor = true;
            this.btnDBpath.Click += new System.EventHandler(this.btnDBpath_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(342, 98);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Back";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSummary
            // 
            this.btnSummary.Location = new System.Drawing.Point(181, 98);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(75, 23);
            this.btnSummary.TabIndex = 13;
            this.btnSummary.Text = "Summary";
            this.btnSummary.UseVisualStyleBackColor = true;
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // btnInstalMySQL
            // 
            this.btnInstalMySQL.Location = new System.Drawing.Point(261, 98);
            this.btnInstalMySQL.Name = "btnInstalMySQL";
            this.btnInstalMySQL.Size = new System.Drawing.Size(75, 23);
            this.btnInstalMySQL.TabIndex = 10;
            this.btnInstalMySQL.Text = "Finish";
            this.btnInstalMySQL.UseVisualStyleBackColor = true;
            this.btnInstalMySQL.Click += new System.EventHandler(this.btnInstalMySQL_Click);
            // 
            // openLicenceKey
            // 
            this.openLicenceKey.FileName = "openFileDialog1";
            // 
            // frmMySQLInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 363);
            this.Controls.Add(this.grpDBInfo);
            this.Controls.Add(this.grpLicense);
            this.Controls.Add(this.grpbxSummary);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMySQLInstaller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MySQL Installer";
            this.Activated += new System.EventHandler(this.frmMySQLInstaller_Activated);
            this.grpbxSummary.ResumeLayout(false);
            this.grpbxSummary.PerformLayout();
            this.grpLicense.ResumeLayout(false);
            this.grpLicense.PerformLayout();
            this.grpDBInfo.ResumeLayout(false);
            this.grpDBInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserMySQLPath;
        private System.Windows.Forms.GroupBox grpbxSummary;
        private System.Windows.Forms.Label lblMySQLInstallStatus;
        private System.Windows.Forms.Label lblMySQLACPERPInstallStatus;
        private System.Windows.Forms.Button btnServiceStatus;
        private System.Windows.Forms.GroupBox grpLicense;
        private System.Windows.Forms.Label lblBrachNameValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBarachCodeValue;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.Label lblLicenceKey;
        private System.Windows.Forms.TextBox txtLicenceKey;
        private System.Windows.Forms.Button BtnBrowse;
        private System.Windows.Forms.Label lblMySQLInstallPath;
        private System.Windows.Forms.GroupBox grpDBInfo;
        private System.Windows.Forms.RadioButton optServer;
        private System.Windows.Forms.RadioButton optClient;
        private System.Windows.Forms.Label lblDataPath;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtDBPath;
        private System.Windows.Forms.TextBox txtHostName;
        private System.Windows.Forms.Label lblInstallationMode;
        private System.Windows.Forms.Label lblDBServer;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnDBpath;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.Button btnInstalMySQL;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.OpenFileDialog openLicenceKey;
    }
}

