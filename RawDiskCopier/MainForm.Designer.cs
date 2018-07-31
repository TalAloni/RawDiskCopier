namespace RawDiskCopier
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.comboSourceDisk = new System.Windows.Forms.ComboBox();
            this.lblSourceDiskSerialNumber = new System.Windows.Forms.Label();
            this.btnCopyLog = new System.Windows.Forms.Button();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.groupProgress = new System.Windows.Forms.GroupBox();
            this.lblUnrecoveredSectors = new System.Windows.Forms.Label();
            this.pictureBoxLegend = new System.Windows.Forms.PictureBox();
            this.groupLegend = new System.Windows.Forms.GroupBox();
            this.lblSourceDisk = new System.Windows.Forms.Label();
            this.lblTargetDisk = new System.Windows.Forms.Label();
            this.comboTargetDisk = new System.Windows.Forms.ComboBox();
            this.lblTargetDiskSerialNumber = new System.Windows.Forms.Label();
            this.chkMaximizeDataRecovery = new System.Windows.Forms.CheckBox();
            this.groupOptions = new System.Windows.Forms.GroupBox();
            this.chkWriteZeros = new System.Windows.Forms.CheckBox();
            this.chkInvalidateWindowsCache = new System.Windows.Forms.CheckBox();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            this.groupProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLegend)).BeginInit();
            this.groupLegend.SuspendLayout();
            this.groupOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.Location = new System.Drawing.Point(6, 79);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(350, 14);
            this.pictureBoxMap.TabIndex = 0;
            this.pictureBoxMap.TabStop = false;
            this.pictureBoxMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxMap_Paint);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(362, 75);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // comboSourceDisk
            // 
            this.comboSourceDisk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSourceDisk.Enabled = false;
            this.comboSourceDisk.FormattingEnabled = true;
            this.comboSourceDisk.Location = new System.Drawing.Point(97, 8);
            this.comboSourceDisk.Name = "comboSourceDisk";
            this.comboSourceDisk.Size = new System.Drawing.Size(260, 21);
            this.comboSourceDisk.TabIndex = 1;
            this.comboSourceDisk.SelectedIndexChanged += new System.EventHandler(this.comboSourceDisk_SelectedIndexChanged);
            // 
            // lblSourceDiskSerialNumber
            // 
            this.lblSourceDiskSerialNumber.AutoSize = true;
            this.lblSourceDiskSerialNumber.Location = new System.Drawing.Point(366, 11);
            this.lblSourceDiskSerialNumber.Name = "lblSourceDiskSerialNumber";
            this.lblSourceDiskSerialNumber.Size = new System.Drawing.Size(30, 13);
            this.lblSourceDiskSerialNumber.TabIndex = 5;
            this.lblSourceDiskSerialNumber.Text = "S/N:";
            // 
            // btnCopyLog
            // 
            this.btnCopyLog.Location = new System.Drawing.Point(487, 75);
            this.btnCopyLog.Name = "btnCopyLog";
            this.btnCopyLog.Size = new System.Drawing.Size(75, 23);
            this.btnCopyLog.TabIndex = 7;
            this.btnCopyLog.Text = "Copy Log";
            this.btnCopyLog.UseVisualStyleBackColor = true;
            this.btnCopyLog.Click += new System.EventHandler(this.btnCopyLog_Click);
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(3, 36);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(47, 13);
            this.lblPosition.TabIndex = 7;
            this.lblPosition.Text = "Position:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(3, 16);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(41, 13);
            this.lblSpeed.TabIndex = 8;
            this.lblSpeed.Text = "Speed:";
            // 
            // groupProgress
            // 
            this.groupProgress.Controls.Add(this.lblUnrecoveredSectors);
            this.groupProgress.Controls.Add(this.lblSpeed);
            this.groupProgress.Controls.Add(this.lblPosition);
            this.groupProgress.Location = new System.Drawing.Point(6, 100);
            this.groupProgress.Name = "groupProgress";
            this.groupProgress.Size = new System.Drawing.Size(173, 74);
            this.groupProgress.TabIndex = 8;
            this.groupProgress.TabStop = false;
            this.groupProgress.Text = "Source Disk";
            // 
            // lblUnrecoveredSectors
            // 
            this.lblUnrecoveredSectors.AutoSize = true;
            this.lblUnrecoveredSectors.Location = new System.Drawing.Point(3, 56);
            this.lblUnrecoveredSectors.Name = "lblUnrecoveredSectors";
            this.lblUnrecoveredSectors.Size = new System.Drawing.Size(111, 13);
            this.lblUnrecoveredSectors.TabIndex = 9;
            this.lblUnrecoveredSectors.Text = "Unrecovered Sectors:";
            // 
            // pictureBoxLegend
            // 
            this.pictureBoxLegend.Location = new System.Drawing.Point(6, 13);
            this.pictureBoxLegend.Name = "pictureBoxLegend";
            this.pictureBoxLegend.Size = new System.Drawing.Size(163, 56);
            this.pictureBoxLegend.TabIndex = 10;
            this.pictureBoxLegend.TabStop = false;
            this.pictureBoxLegend.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxLegend_Paint);
            // 
            // groupLegend
            // 
            this.groupLegend.Controls.Add(this.pictureBoxLegend);
            this.groupLegend.Location = new System.Drawing.Point(184, 100);
            this.groupLegend.Name = "groupLegend";
            this.groupLegend.Size = new System.Drawing.Size(173, 74);
            this.groupLegend.TabIndex = 9;
            this.groupLegend.TabStop = false;
            this.groupLegend.Text = "Legend";
            // 
            // lblSourceDisk
            // 
            this.lblSourceDisk.AutoSize = true;
            this.lblSourceDisk.Location = new System.Drawing.Point(4, 11);
            this.lblSourceDisk.Name = "lblSourceDisk";
            this.lblSourceDisk.Size = new System.Drawing.Size(68, 13);
            this.lblSourceDisk.TabIndex = 12;
            this.lblSourceDisk.Text = "Source Disk:";
            // 
            // lblTargetDisk
            // 
            this.lblTargetDisk.AutoSize = true;
            this.lblTargetDisk.Location = new System.Drawing.Point(4, 45);
            this.lblTargetDisk.Name = "lblTargetDisk";
            this.lblTargetDisk.Size = new System.Drawing.Size(65, 13);
            this.lblTargetDisk.TabIndex = 13;
            this.lblTargetDisk.Text = "Target Disk:";
            // 
            // comboTargetDisk
            // 
            this.comboTargetDisk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTargetDisk.Enabled = false;
            this.comboTargetDisk.FormattingEnabled = true;
            this.comboTargetDisk.Location = new System.Drawing.Point(97, 42);
            this.comboTargetDisk.Name = "comboTargetDisk";
            this.comboTargetDisk.Size = new System.Drawing.Size(260, 21);
            this.comboTargetDisk.TabIndex = 2;
            this.comboTargetDisk.SelectedIndexChanged += new System.EventHandler(this.comboTargetDisk_SelectedIndexChanged);
            // 
            // lblTargetDiskSerialNumber
            // 
            this.lblTargetDiskSerialNumber.AutoSize = true;
            this.lblTargetDiskSerialNumber.Location = new System.Drawing.Point(366, 45);
            this.lblTargetDiskSerialNumber.Name = "lblTargetDiskSerialNumber";
            this.lblTargetDiskSerialNumber.Size = new System.Drawing.Size(30, 13);
            this.lblTargetDiskSerialNumber.TabIndex = 15;
            this.lblTargetDiskSerialNumber.Text = "S/N:";
            // 
            // chkMaximizeDataRecovery
            // 
            this.chkMaximizeDataRecovery.AutoSize = true;
            this.chkMaximizeDataRecovery.Location = new System.Drawing.Point(6, 15);
            this.chkMaximizeDataRecovery.Name = "chkMaximizeDataRecovery";
            this.chkMaximizeDataRecovery.Size = new System.Drawing.Size(167, 17);
            this.chkMaximizeDataRecovery.TabIndex = 16;
            this.chkMaximizeDataRecovery.Text = "Maximize data recovery (slow)";
            this.toolTipMain.SetToolTip(this.chkMaximizeDataRecovery, "If a bulk read operation fails due to damaged sectors, attempt reading sector by " +
                    "sector ");
            this.chkMaximizeDataRecovery.UseVisualStyleBackColor = true;
            // 
            // groupOptions
            // 
            this.groupOptions.Controls.Add(this.chkWriteZeros);
            this.groupOptions.Controls.Add(this.chkInvalidateWindowsCache);
            this.groupOptions.Controls.Add(this.chkMaximizeDataRecovery);
            this.groupOptions.Location = new System.Drawing.Point(362, 100);
            this.groupOptions.Name = "groupOptions";
            this.groupOptions.Size = new System.Drawing.Size(200, 74);
            this.groupOptions.TabIndex = 17;
            this.groupOptions.TabStop = false;
            this.groupOptions.Text = "Options";
            // 
            // chkWriteZeros
            // 
            this.chkWriteZeros.AutoSize = true;
            this.chkWriteZeros.Checked = true;
            this.chkWriteZeros.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWriteZeros.Location = new System.Drawing.Point(6, 35);
            this.chkWriteZeros.Name = "chkWriteZeros";
            this.chkWriteZeros.Size = new System.Drawing.Size(192, 17);
            this.chkWriteZeros.TabIndex = 18;
            this.chkWriteZeros.Text = "Write zeros if sector is unrecovered";
            this.toolTipMain.SetToolTip(this.chkWriteZeros, "If the source sector was not recovered, zero out the target sector (instead of le" +
                    "aving it as is)");
            this.chkWriteZeros.UseVisualStyleBackColor = true;
            // 
            // chkInvalidateWindowsCache
            // 
            this.chkInvalidateWindowsCache.AutoSize = true;
            this.chkInvalidateWindowsCache.Location = new System.Drawing.Point(6, 55);
            this.chkInvalidateWindowsCache.Name = "chkInvalidateWindowsCache";
            this.chkInvalidateWindowsCache.Size = new System.Drawing.Size(169, 17);
            this.chkInvalidateWindowsCache.TabIndex = 17;
            this.chkInvalidateWindowsCache.Text = "Notify Windows on completion";
            this.toolTipMain.SetToolTip(this.chkInvalidateWindowsCache, "Invalidate the cached partition table and re-enumerates the device on completion");
            this.chkInvalidateWindowsCache.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 183);
            this.Controls.Add(this.groupOptions);
            this.Controls.Add(this.lblTargetDiskSerialNumber);
            this.Controls.Add(this.comboTargetDisk);
            this.Controls.Add(this.lblTargetDisk);
            this.Controls.Add(this.lblSourceDisk);
            this.Controls.Add(this.groupLegend);
            this.Controls.Add(this.btnCopyLog);
            this.Controls.Add(this.groupProgress);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblSourceDiskSerialNumber);
            this.Controls.Add(this.comboSourceDisk);
            this.Controls.Add(this.pictureBoxMap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(576, 210);
            this.MinimumSize = new System.Drawing.Size(576, 210);
            this.Name = "MainForm";
            this.Text = "Tal Aloni\'s Raw Disk Copier";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
            this.groupProgress.ResumeLayout(false);
            this.groupProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLegend)).EndInit();
            this.groupLegend.ResumeLayout(false);
            this.groupOptions.ResumeLayout(false);
            this.groupOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMap;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox comboSourceDisk;
        private System.Windows.Forms.Label lblSourceDiskSerialNumber;
        private System.Windows.Forms.Button btnCopyLog;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.GroupBox groupProgress;
        private System.Windows.Forms.PictureBox pictureBoxLegend;
        private System.Windows.Forms.GroupBox groupLegend;
        private System.Windows.Forms.Label lblSourceDisk;
        private System.Windows.Forms.Label lblTargetDisk;
        private System.Windows.Forms.ComboBox comboTargetDisk;
        private System.Windows.Forms.Label lblTargetDiskSerialNumber;
        private System.Windows.Forms.Label lblUnrecoveredSectors;
        private System.Windows.Forms.CheckBox chkMaximizeDataRecovery;
        private System.Windows.Forms.GroupBox groupOptions;
        private System.Windows.Forms.CheckBox chkInvalidateWindowsCache;
        private System.Windows.Forms.CheckBox chkWriteZeros;
        private System.Windows.Forms.ToolTip toolTipMain;
    }
}

