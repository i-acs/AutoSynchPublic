namespace FileSync
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SystrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SystrayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.gbSourceFolder = new System.Windows.Forms.GroupBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.tbSourcefolder = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbTargetFolder = new System.Windows.Forms.TextBox();
            this.cbInterval = new System.Windows.Forms.ComboBox();
            this.gbInterval = new System.Windows.Forms.GroupBox();
            this.gbServer = new System.Windows.Forms.GroupBox();
            this.lblServerPort = new System.Windows.Forms.Label();
            this.tbServerPort = new System.Windows.Forms.TextBox();
            this.lblServerIpAddress = new System.Windows.Forms.Label();
            this.tbServerIp = new System.Windows.Forms.TextBox();
            this.pbTransfer = new System.Windows.Forms.ProgressBar();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.gbSpeed = new System.Windows.Forms.GroupBox();
            this.tbSpeed = new System.Windows.Forms.TextBox();
            this.gbNumTransfers = new System.Windows.Forms.GroupBox();
            this.tbFilesAtOnce = new System.Windows.Forms.TextBox();
            this.gbUniqueId = new System.Windows.Forms.GroupBox();
            this.tbUniqueId = new System.Windows.Forms.TextBox();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpConfiguration = new System.Windows.Forms.TabPage();
            this.gbFileCopyLatency = new System.Windows.Forms.GroupBox();
            this.tbFileCopyLatency = new System.Windows.Forms.TextBox();
            this.gbSkipLatency = new System.Windows.Forms.GroupBox();
            this.tbSkipLatency = new System.Windows.Forms.TextBox();
            this.tpfiles = new System.Windows.Forms.TabPage();
            this.tpServer = new System.Windows.Forms.TabPage();
            this.gbServerDetails = new System.Windows.Forms.GroupBox();
            this.gbServerConfig = new System.Windows.Forms.GroupBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.tbLocalPort = new System.Windows.Forms.TextBox();
            this.cbLocalIpAddresses = new System.Windows.Forms.ComboBox();
            this.tpErrorLog1 = new System.Windows.Forms.TabPage();
            this.lbErrorMsgs = new System.Windows.Forms.ListBox();
            this.gbOverallProgress = new System.Windows.Forms.GroupBox();
            this.lbServerMessages = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SystrayIconMenu.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbSourceFolder.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbInterval.SuspendLayout();
            this.gbServer.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.gbSpeed.SuspendLayout();
            this.gbNumTransfers.SuspendLayout();
            this.gbUniqueId.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpConfiguration.SuspendLayout();
            this.gbFileCopyLatency.SuspendLayout();
            this.gbSkipLatency.SuspendLayout();
            this.tpServer.SuspendLayout();
            this.gbServerDetails.SuspendLayout();
            this.gbServerConfig.SuspendLayout();
            this.tpErrorLog1.SuspendLayout();
            this.gbOverallProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 86);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::FileSync.Properties.Resources.system_run_3;
            this.pictureBox1.Location = new System.Drawing.Point(430, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 77);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Synchronization";
            // 
            // SystrayIcon
            // 
            this.SystrayIcon.ContextMenuStrip = this.SystrayIconMenu;
            this.SystrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("SystrayIcon.Icon")));
            this.SystrayIcon.Text = "File Sync Configuration";
            this.SystrayIcon.Visible = true;
            // 
            // SystrayIconMenu
            // 
            this.SystrayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.SystrayIconMenu.Name = "SystrayIconMenu";
            this.SystrayIconMenu.Size = new System.Drawing.Size(128, 98);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.configureToolStripMenuItem.Text = "Configure";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.configureToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Controls.Add(this.btSave);
            this.panel2.Location = new System.Drawing.Point(-2, 523);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 35);
            this.panel2.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(3, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(421, 23);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Idle.";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Location = new System.Drawing.Point(430, 3);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 2;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // gbSourceFolder
            // 
            this.gbSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSourceFolder.Controls.Add(this.btBrowse);
            this.gbSourceFolder.Controls.Add(this.tbSourcefolder);
            this.gbSourceFolder.Location = new System.Drawing.Point(14, 61);
            this.gbSourceFolder.Name = "gbSourceFolder";
            this.gbSourceFolder.Size = new System.Drawing.Size(470, 49);
            this.gbSourceFolder.TabIndex = 3;
            this.gbSourceFolder.TabStop = false;
            this.gbSourceFolder.Text = "Source Folder (Files on this machine):";
            // 
            // btBrowse
            // 
            this.btBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowse.Location = new System.Drawing.Point(410, 17);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(53, 23);
            this.btBrowse.TabIndex = 5;
            this.btBrowse.Text = "Browse";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // tbSourcefolder
            // 
            this.tbSourcefolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSourcefolder.Location = new System.Drawing.Point(12, 19);
            this.tbSourcefolder.Name = "tbSourcefolder";
            this.tbSourcefolder.Size = new System.Drawing.Size(392, 20);
            this.tbSourcefolder.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbTargetFolder);
            this.groupBox1.Location = new System.Drawing.Point(14, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 49);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target Folder (Where they will be copied to on the server):";
            // 
            // tbTargetFolder
            // 
            this.tbTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTargetFolder.Location = new System.Drawing.Point(12, 19);
            this.tbTargetFolder.Name = "tbTargetFolder";
            this.tbTargetFolder.Size = new System.Drawing.Size(452, 20);
            this.tbTargetFolder.TabIndex = 4;
            // 
            // cbInterval
            // 
            this.cbInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterval.FormattingEnabled = true;
            this.cbInterval.Items.AddRange(new object[] {
            "Every 1 minute",
            "Every 5 minutes",
            "Every 10 minutes",
            "Every 15 minutes",
            "Every 30 minutes",
            "Every 60 minutes",
            "Every 2 Hours",
            "Every 6 Hours",
            "Every 12 Hours",
            "Every 24 Hours"});
            this.cbInterval.Location = new System.Drawing.Point(12, 19);
            this.cbInterval.Name = "cbInterval";
            this.cbInterval.Size = new System.Drawing.Size(157, 21);
            this.cbInterval.TabIndex = 5;
            // 
            // gbInterval
            // 
            this.gbInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInterval.Controls.Add(this.cbInterval);
            this.gbInterval.Location = new System.Drawing.Point(14, 226);
            this.gbInterval.Name = "gbInterval";
            this.gbInterval.Size = new System.Drawing.Size(175, 49);
            this.gbInterval.TabIndex = 6;
            this.gbInterval.TabStop = false;
            this.gbInterval.Text = "Sync Interval";
            // 
            // gbServer
            // 
            this.gbServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbServer.Controls.Add(this.lblServerPort);
            this.gbServer.Controls.Add(this.tbServerPort);
            this.gbServer.Controls.Add(this.lblServerIpAddress);
            this.gbServer.Controls.Add(this.tbServerIp);
            this.gbServer.Location = new System.Drawing.Point(14, 281);
            this.gbServer.Name = "gbServer";
            this.gbServer.Size = new System.Drawing.Size(470, 67);
            this.gbServer.TabIndex = 7;
            this.gbServer.TabStop = false;
            this.gbServer.Text = "Server Connection Details:";
            // 
            // lblServerPort
            // 
            this.lblServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServerPort.AutoSize = true;
            this.lblServerPort.Location = new System.Drawing.Point(341, 18);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(63, 13);
            this.lblServerPort.TabIndex = 3;
            this.lblServerPort.Text = "Server Port:";
            // 
            // tbServerPort
            // 
            this.tbServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServerPort.Location = new System.Drawing.Point(342, 34);
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Size = new System.Drawing.Size(122, 20);
            this.tbServerPort.TabIndex = 2;
            // 
            // lblServerIpAddress
            // 
            this.lblServerIpAddress.AutoSize = true;
            this.lblServerIpAddress.Location = new System.Drawing.Point(11, 18);
            this.lblServerIpAddress.Name = "lblServerIpAddress";
            this.lblServerIpAddress.Size = new System.Drawing.Size(94, 13);
            this.lblServerIpAddress.TabIndex = 1;
            this.lblServerIpAddress.Text = "Server Ip Address:";
            // 
            // tbServerIp
            // 
            this.tbServerIp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServerIp.Location = new System.Drawing.Point(12, 34);
            this.tbServerIp.Name = "tbServerIp";
            this.tbServerIp.Size = new System.Drawing.Size(324, 20);
            this.tbServerIp.TabIndex = 0;
            // 
            // pbTransfer
            // 
            this.pbTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTransfer.Location = new System.Drawing.Point(6, 19);
            this.pbTransfer.Name = "pbTransfer";
            this.pbTransfer.Size = new System.Drawing.Size(486, 10);
            this.pbTransfer.TabIndex = 8;
            // 
            // gbFilter
            // 
            this.gbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFilter.Controls.Add(this.tbFilter);
            this.gbFilter.Location = new System.Drawing.Point(14, 171);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(175, 49);
            this.gbFilter.TabIndex = 9;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Search String ( Format like: *.txt )";
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Location = new System.Drawing.Point(12, 19);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(157, 20);
            this.tbFilter.TabIndex = 4;
            // 
            // gbSpeed
            // 
            this.gbSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSpeed.Controls.Add(this.tbSpeed);
            this.gbSpeed.Location = new System.Drawing.Point(311, 171);
            this.gbSpeed.Name = "gbSpeed";
            this.gbSpeed.Size = new System.Drawing.Size(173, 49);
            this.gbSpeed.TabIndex = 10;
            this.gbSpeed.TabStop = false;
            this.gbSpeed.Text = "MegaBITS per second per file:";
            // 
            // tbSpeed
            // 
            this.tbSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSpeed.Location = new System.Drawing.Point(12, 19);
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(155, 20);
            this.tbSpeed.TabIndex = 4;
            this.tbSpeed.Text = "1000";
            // 
            // gbNumTransfers
            // 
            this.gbNumTransfers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNumTransfers.Controls.Add(this.tbFilesAtOnce);
            this.gbNumTransfers.Location = new System.Drawing.Point(195, 171);
            this.gbNumTransfers.Name = "gbNumTransfers";
            this.gbNumTransfers.Size = new System.Drawing.Size(110, 49);
            this.gbNumTransfers.TabIndex = 11;
            this.gbNumTransfers.TabStop = false;
            this.gbNumTransfers.Text = "Files at once:";
            // 
            // tbFilesAtOnce
            // 
            this.tbFilesAtOnce.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilesAtOnce.Location = new System.Drawing.Point(12, 19);
            this.tbFilesAtOnce.Name = "tbFilesAtOnce";
            this.tbFilesAtOnce.Size = new System.Drawing.Size(92, 20);
            this.tbFilesAtOnce.TabIndex = 4;
            this.tbFilesAtOnce.Text = "5";
            // 
            // gbUniqueId
            // 
            this.gbUniqueId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUniqueId.Controls.Add(this.tbUniqueId);
            this.gbUniqueId.Location = new System.Drawing.Point(15, 6);
            this.gbUniqueId.Name = "gbUniqueId";
            this.gbUniqueId.Size = new System.Drawing.Size(469, 49);
            this.gbUniqueId.TabIndex = 12;
            this.gbUniqueId.TabStop = false;
            this.gbUniqueId.Text = "The unique Id of this file transfer client:";
            // 
            // tbUniqueId
            // 
            this.tbUniqueId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUniqueId.Location = new System.Drawing.Point(12, 19);
            this.tbUniqueId.Name = "tbUniqueId";
            this.tbUniqueId.Size = new System.Drawing.Size(449, 20);
            this.tbUniqueId.TabIndex = 4;
            this.tbUniqueId.Text = "FileTransferClient1";
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpConfiguration);
            this.tcMain.Controls.Add(this.tpfiles);
            this.tcMain.Controls.Add(this.tpServer);
            this.tcMain.Controls.Add(this.tpErrorLog1);
            this.tcMain.Location = new System.Drawing.Point(6, 88);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(498, 388);
            this.tcMain.TabIndex = 13;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tpConfiguration
            // 
            this.tpConfiguration.Controls.Add(this.gbFileCopyLatency);
            this.tpConfiguration.Controls.Add(this.gbSkipLatency);
            this.tpConfiguration.Controls.Add(this.gbUniqueId);
            this.tpConfiguration.Controls.Add(this.gbSourceFolder);
            this.tpConfiguration.Controls.Add(this.groupBox1);
            this.tpConfiguration.Controls.Add(this.gbSpeed);
            this.tpConfiguration.Controls.Add(this.gbNumTransfers);
            this.tpConfiguration.Controls.Add(this.gbInterval);
            this.tpConfiguration.Controls.Add(this.gbServer);
            this.tpConfiguration.Controls.Add(this.gbFilter);
            this.tpConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tpConfiguration.Name = "tpConfiguration";
            this.tpConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfiguration.Size = new System.Drawing.Size(490, 362);
            this.tpConfiguration.TabIndex = 0;
            this.tpConfiguration.Text = "Configuration";
            this.tpConfiguration.UseVisualStyleBackColor = true;
            // 
            // gbFileCopyLatency
            // 
            this.gbFileCopyLatency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFileCopyLatency.Controls.Add(this.tbFileCopyLatency);
            this.gbFileCopyLatency.Location = new System.Drawing.Point(344, 226);
            this.gbFileCopyLatency.Name = "gbFileCopyLatency";
            this.gbFileCopyLatency.Size = new System.Drawing.Size(140, 49);
            this.gbFileCopyLatency.TabIndex = 14;
            this.gbFileCopyLatency.TabStop = false;
            this.gbFileCopyLatency.Text = "Copy Latency (Millisecs)";
            // 
            // tbFileCopyLatency
            // 
            this.tbFileCopyLatency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileCopyLatency.Location = new System.Drawing.Point(12, 19);
            this.tbFileCopyLatency.Name = "tbFileCopyLatency";
            this.tbFileCopyLatency.Size = new System.Drawing.Size(122, 20);
            this.tbFileCopyLatency.TabIndex = 4;
            this.tbFileCopyLatency.Text = "100";
            // 
            // gbSkipLatency
            // 
            this.gbSkipLatency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSkipLatency.Controls.Add(this.tbSkipLatency);
            this.gbSkipLatency.Location = new System.Drawing.Point(195, 226);
            this.gbSkipLatency.Name = "gbSkipLatency";
            this.gbSkipLatency.Size = new System.Drawing.Size(143, 49);
            this.gbSkipLatency.TabIndex = 13;
            this.gbSkipLatency.TabStop = false;
            this.gbSkipLatency.Text = "Skip Latency (Millisecs)";
            // 
            // tbSkipLatency
            // 
            this.tbSkipLatency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSkipLatency.Location = new System.Drawing.Point(12, 19);
            this.tbSkipLatency.Name = "tbSkipLatency";
            this.tbSkipLatency.Size = new System.Drawing.Size(125, 20);
            this.tbSkipLatency.TabIndex = 4;
            this.tbSkipLatency.Text = "100";
            // 
            // tpfiles
            // 
            this.tpfiles.AutoScroll = true;
            this.tpfiles.Location = new System.Drawing.Point(4, 22);
            this.tpfiles.Name = "tpfiles";
            this.tpfiles.Padding = new System.Windows.Forms.Padding(3);
            this.tpfiles.Size = new System.Drawing.Size(490, 362);
            this.tpfiles.TabIndex = 1;
            this.tpfiles.Text = "Outgoing Files:";
            this.tpfiles.UseVisualStyleBackColor = true;
            this.tpfiles.Click += new System.EventHandler(this.tpfiles_Click);
            // 
            // tpServer
            // 
            this.tpServer.Controls.Add(this.gbServerDetails);
            this.tpServer.Controls.Add(this.gbServerConfig);
            this.tpServer.Location = new System.Drawing.Point(4, 22);
            this.tpServer.Name = "tpServer";
            this.tpServer.Padding = new System.Windows.Forms.Padding(3);
            this.tpServer.Size = new System.Drawing.Size(490, 362);
            this.tpServer.TabIndex = 3;
            this.tpServer.Text = "Incoming connections";
            this.tpServer.UseVisualStyleBackColor = true;
            this.tpServer.Click += new System.EventHandler(this.tpServer_Click);
            // 
            // gbServerDetails
            // 
            this.gbServerDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbServerDetails.Controls.Add(this.lbServerMessages);
            this.gbServerDetails.Location = new System.Drawing.Point(8, 78);
            this.gbServerDetails.Name = "gbServerDetails";
            this.gbServerDetails.Size = new System.Drawing.Size(476, 278);
            this.gbServerDetails.TabIndex = 1;
            this.gbServerDetails.TabStop = false;
            this.gbServerDetails.Text = "Connection Details";
            // 
            // gbServerConfig
            // 
            this.gbServerConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbServerConfig.Controls.Add(this.lblPort);
            this.gbServerConfig.Controls.Add(this.lblIpAddress);
            this.gbServerConfig.Controls.Add(this.tbLocalPort);
            this.gbServerConfig.Controls.Add(this.cbLocalIpAddresses);
            this.gbServerConfig.Location = new System.Drawing.Point(8, 6);
            this.gbServerConfig.Name = "gbServerConfig";
            this.gbServerConfig.Size = new System.Drawing.Size(476, 66);
            this.gbServerConfig.TabIndex = 0;
            this.gbServerConfig.TabStop = false;
            this.gbServerConfig.Text = "Configuration";
            this.gbServerConfig.Enter += new System.EventHandler(this.gbServerConfig_Enter);
            // 
            // lblPort
            // 
            this.lblPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(368, 23);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(94, 13);
            this.lblPort.TabIndex = 3;
            this.lblPort.Text = "Listen on this Port:";
            this.lblPort.Click += new System.EventHandler(this.lblPort_Click);
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.AutoSize = true;
            this.lblIpAddress.Location = new System.Drawing.Point(23, 23);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(126, 13);
            this.lblIpAddress.TabIndex = 2;
            this.lblIpAddress.Text = "Listen on this IP Address:";
            // 
            // tbLocalPort
            // 
            this.tbLocalPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocalPort.Location = new System.Drawing.Point(369, 40);
            this.tbLocalPort.Name = "tbLocalPort";
            this.tbLocalPort.Size = new System.Drawing.Size(100, 20);
            this.tbLocalPort.TabIndex = 1;
            this.tbLocalPort.Text = "15503";
            // 
            // cbLocalIpAddresses
            // 
            this.cbLocalIpAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLocalIpAddresses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocalIpAddresses.FormattingEnabled = true;
            this.cbLocalIpAddresses.Location = new System.Drawing.Point(24, 39);
            this.cbLocalIpAddresses.Name = "cbLocalIpAddresses";
            this.cbLocalIpAddresses.Size = new System.Drawing.Size(339, 21);
            this.cbLocalIpAddresses.TabIndex = 0;
            // 
            // tpErrorLog1
            // 
            this.tpErrorLog1.Controls.Add(this.lbErrorMsgs);
            this.tpErrorLog1.Location = new System.Drawing.Point(4, 22);
            this.tpErrorLog1.Name = "tpErrorLog1";
            this.tpErrorLog1.Padding = new System.Windows.Forms.Padding(3);
            this.tpErrorLog1.Size = new System.Drawing.Size(490, 362);
            this.tpErrorLog1.TabIndex = 4;
            this.tpErrorLog1.Text = "Error Log";
            this.tpErrorLog1.UseVisualStyleBackColor = true;
            // 
            // lbErrorMsgs
            // 
            this.lbErrorMsgs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbErrorMsgs.FormattingEnabled = true;
            this.lbErrorMsgs.Location = new System.Drawing.Point(8, 6);
            this.lbErrorMsgs.Name = "lbErrorMsgs";
            this.lbErrorMsgs.Size = new System.Drawing.Size(476, 342);
            this.lbErrorMsgs.TabIndex = 0;
            // 
            // gbOverallProgress
            // 
            this.gbOverallProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOverallProgress.Controls.Add(this.pbTransfer);
            this.gbOverallProgress.Location = new System.Drawing.Point(6, 481);
            this.gbOverallProgress.Name = "gbOverallProgress";
            this.gbOverallProgress.Size = new System.Drawing.Size(498, 34);
            this.gbOverallProgress.TabIndex = 14;
            this.gbOverallProgress.TabStop = false;
            this.gbOverallProgress.Text = "Over All Progress:";
            // 
            // lbServerMessages
            // 
            this.lbServerMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbServerMessages.FormattingEnabled = true;
            this.lbServerMessages.Location = new System.Drawing.Point(6, 19);
            this.lbServerMessages.Name = "lbServerMessages";
            this.lbServerMessages.Size = new System.Drawing.Size(463, 251);
            this.lbServerMessages.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 556);
            this.Controls.Add(this.gbOverallProgress);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(381, 408);
            this.Name = "frmMain";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Synch Utility";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.SystrayIconMenu.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gbSourceFolder.ResumeLayout(false);
            this.gbSourceFolder.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbInterval.ResumeLayout(false);
            this.gbServer.ResumeLayout(false);
            this.gbServer.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.gbSpeed.ResumeLayout(false);
            this.gbSpeed.PerformLayout();
            this.gbNumTransfers.ResumeLayout(false);
            this.gbNumTransfers.PerformLayout();
            this.gbUniqueId.ResumeLayout(false);
            this.gbUniqueId.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tpConfiguration.ResumeLayout(false);
            this.gbFileCopyLatency.ResumeLayout(false);
            this.gbFileCopyLatency.PerformLayout();
            this.gbSkipLatency.ResumeLayout(false);
            this.gbSkipLatency.PerformLayout();
            this.tpServer.ResumeLayout(false);
            this.gbServerDetails.ResumeLayout(false);
            this.gbServerConfig.ResumeLayout(false);
            this.gbServerConfig.PerformLayout();
            this.tpErrorLog1.ResumeLayout(false);
            this.gbOverallProgress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon SystrayIcon;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ContextMenuStrip SystrayIconMenu;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbSourceFolder;
        private System.Windows.Forms.TextBox tbSourcefolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbTargetFolder;
        private System.Windows.Forms.ComboBox cbInterval;
        private System.Windows.Forms.GroupBox gbInterval;
        private System.Windows.Forms.GroupBox gbServer;
        private System.Windows.Forms.Label lblServerPort;
        private System.Windows.Forms.TextBox tbServerPort;
        private System.Windows.Forms.Label lblServerIpAddress;
        private System.Windows.Forms.TextBox tbServerIp;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.ProgressBar pbTransfer;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.GroupBox gbSpeed;
        private System.Windows.Forms.TextBox tbSpeed;
        private System.Windows.Forms.GroupBox gbNumTransfers;
        private System.Windows.Forms.TextBox tbFilesAtOnce;
        private System.Windows.Forms.GroupBox gbUniqueId;
        private System.Windows.Forms.TextBox tbUniqueId;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpConfiguration;
        private System.Windows.Forms.TabPage tpfiles;
        private System.Windows.Forms.GroupBox gbOverallProgress;
        private System.Windows.Forms.GroupBox gbFileCopyLatency;
        private System.Windows.Forms.TextBox tbFileCopyLatency;
        private System.Windows.Forms.GroupBox gbSkipLatency;
        private System.Windows.Forms.TextBox tbSkipLatency;
        private System.Windows.Forms.TabPage tpServer;
        private System.Windows.Forms.GroupBox gbServerDetails;
        private System.Windows.Forms.GroupBox gbServerConfig;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblIpAddress;
        private System.Windows.Forms.TextBox tbLocalPort;
        private System.Windows.Forms.ComboBox cbLocalIpAddresses;
        private System.Windows.Forms.TabPage tpErrorLog1;
        private System.Windows.Forms.ListBox lbErrorMsgs;
        private System.Windows.Forms.ListBox lbServerMessages;
    }
}

