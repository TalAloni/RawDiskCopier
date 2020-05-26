/* Copyright (C) 2016-2020 Tal Aloni <tal.aloni.il@gmail.com>. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DiskAccessLibrary;
using Utilities;

namespace RawDiskCopier
{
    public partial class MainForm : Form
    {
        public const int NumberOfUIBlocks = 350;

        private DateTime m_startTime;
        private BlockStatus[] m_blocks = new BlockStatus[NumberOfUIBlocks];
        private bool m_isBusy = false;
        private DiskCopier m_diskCopier;
        private bool m_isClosing = false;
        private List<string> m_log = new List<string>();
        
        public MainForm()
        {
            // Make the GUI ignore the DPI setting
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();
            WinFormsUtils.SetFixedClientSize(this, 568, 183);
            this.Text += " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            List<PhysicalDisk> disks = PhysicalDiskHelper.GetPhysicalDisks();
            PopulateDiskList(disks, comboSourceDisk);
            PopulateDiskList(disks, comboTargetDisk);
        }

        private void pictureBoxMap_Paint(object sender, PaintEventArgs e)
        {
            DrawProgress(e.Graphics);
        }

        private void pictureBoxLegend_Paint(object sender, PaintEventArgs e)
        {
            DrawLegend(e.Graphics);
        }

        private void DrawProgress(Graphics graphics)
        {
            const int LineHeight = 14;

            for (int index = 0; index < m_blocks.Length; index++)
            {
                int x = index;
                SolidBrush brush = new SolidBrush(UIHelper.GetColor(m_blocks[index]));
                graphics.FillRectangle(brush, x, 0, 1, LineHeight);
            }
        }

        private void DrawLegend(Graphics graphics)
        {
            DrawLegendEntry(graphics, 1, 8, BlockStatus.OK, "OK");
            DrawLegendEntry(graphics, 1, 24, BlockStatus.Damaged, "Damaged");
            DrawLegendEntry(graphics, 1, 40, BlockStatus.IOError, "IO Error");
        }

        private void DrawLegendEntry(Graphics graphics, float x, float y, BlockStatus status, string text)
        {
            const int BlockWidth = 4;
            const int BlockHeight = 9;

            SolidBrush brush = new SolidBrush(UIHelper.GetColor(status));
            graphics.FillRectangle(brush, x + 1, y - 1, BlockWidth, BlockHeight);
            Font font = new Font(FontFamily.GenericSansSerif, 8);
            graphics.DrawString(text, font, Brushes.Black, x + 12, y - 3);
        }

        private void comboSourceDisk_SelectedIndexChanged(object sender, EventArgs e)
        {
            int physicalDiskIndex = ((KeyValuePair<int, string>)comboSourceDisk.SelectedItem).Key;
            if (physicalDiskIndex == -1) // VHD
            {
                DialogResult openFileResult = openVirtualDiskFileDialog.ShowDialog();
                if (openFileResult != DialogResult.OK)
                {
                    comboSourceDisk.SelectedIndex = 0;
                }
                else
                {
                    lblSourceDiskSerialNumber.Text = openVirtualDiskFileDialog.FileName;
                }
            }
            else
            {
                PhysicalDisk disk = null;
                try
                {
                    disk = new PhysicalDisk(physicalDiskIndex);
                }
                catch (DriveNotFoundException)
                {
                }

                if (disk != null)
                {
                    lblSourceDiskSerialNumber.Text = "S/N: " + disk.SerialNumber;
                }
                else
                {
                    lblSourceDiskSerialNumber.Text = "Error: Disk not found";
                }
            }
        }

        private void comboTargetDisk_SelectedIndexChanged(object sender, EventArgs e)
        {
            int physicalDiskIndex = ((KeyValuePair<int, string>)comboTargetDisk.SelectedItem).Key;
            if (physicalDiskIndex == -1) // VHD
            {
                DialogResult openFileResult = saveVirtualDiskFileDialog.ShowDialog();
                if (openFileResult != DialogResult.OK)
                {
                    comboTargetDisk.SelectedIndex = 0;
                }
                else
                {
                    lblTargetDiskSerialNumber.Text = saveVirtualDiskFileDialog.FileName;
                }
            }
            else
            {
                PhysicalDisk disk = null;
                try
                {
                    disk = new PhysicalDisk(physicalDiskIndex);
                }
                catch (DriveNotFoundException)
                {
                }

                if (disk != null)
                {
                    lblTargetDiskSerialNumber.Text = "S/N: " + disk.SerialNumber;
                }
                else
                {
                    lblTargetDiskSerialNumber.Text = "Error: Disk not found";
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_isBusy)
            {
                e.Cancel = true;
                m_diskCopier.Abort = true;
                m_isClosing = true;
            }
        }

        private void btnCopyLog_Click(object sender, EventArgs e)
        {
            if (m_log.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (string message in m_log)
                {
                    builder.AppendLine(message);
                }
                Clipboard.SetText(builder.ToString());
            }
        }

        private void PopulateDiskList(List<PhysicalDisk> disks, ComboBox comboBox)
        {
            Thread thread = new Thread(delegate()
            {
                this.Invoke((MethodInvoker)delegate
                {
                    comboBox.Items.Clear();
                    comboBox.DisplayMember = "Value";
                    comboBox.ValueMember = "Key";
                    foreach (PhysicalDisk disk in disks)
                    {
                        string title = String.Format("[{0}] {1} ({2})", disk.PhysicalDiskIndex, disk.Description,  UIHelper.GetSizeString(disk.Size));
                        comboBox.Items.Add(new KeyValuePair<int, string>(disk.PhysicalDiskIndex, title));
                    }

                    comboBox.Items.Add(new KeyValuePair<int, string>(-1, "Virtual Hard Disk"));
                    comboBox.SelectedIndex = 0;
                    comboBox.Enabled = true;
                });
            });
            thread.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (m_isBusy)
            {
                btnStart.Enabled = false;
                btnStart.Text = "Stopping";
                groupOptions.Enabled = false;
                Thread thread = new Thread(delegate()
                {
                    m_diskCopier.Abort = true;
                    while (m_isBusy)
                    {
                        Thread.Sleep(100);
                    }
                    if (!m_isClosing)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            EnableUI();
                            btnStart.Enabled = true;
                        });
                    }
                });
                thread.Start();
            }
            else
            {
                if (comboSourceDisk.SelectedItem == null)
                {
                    return;
                }
                int sourceDiskIndex = ((KeyValuePair<int, string>)comboSourceDisk.SelectedItem).Key;
                int targetDiskIndex = ((KeyValuePair<int, string>)comboTargetDisk.SelectedItem).Key;
                Disk sourceDisk = null;
                Disk targetDisk = null;
                if (sourceDiskIndex == -1)
                {
                    string path = openVirtualDiskFileDialog.FileName;
                    try
                    {
                        sourceDisk = new VirtualHardDisk(path, true);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Failed to open the virtual disk: " + ex.Message, "Error");
                        return;
                    }
                }
                else
                {
                    try
                    {
                        sourceDisk = new PhysicalDisk(sourceDiskIndex);
                    }
                    catch (DriveNotFoundException)
                    {
                        MessageBox.Show("Source disk not found", "Error");
                        return;
                    }
                }

                if (targetDiskIndex == -1)
                {
                    string path = saveVirtualDiskFileDialog.FileName; ;
                    try
                    {
                        if (chkWriteZeros.Checked)
                        {
                            targetDisk = VirtualHardDisk.CreateFixedDisk(path, sourceDisk.Size);
                        }
                        else
                        {
                            targetDisk = VirtualHardDisk.CreateDynamicDisk(path, sourceDisk.Size);
                        }
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Failed to create the virtual disk: " + ex.Message, "Error");
                        return;
                    }
                }
                else
                {
                    try
                    {
                        targetDisk = new PhysicalDisk(targetDiskIndex);
                    }
                    catch (DriveNotFoundException)
                    {
                        MessageBox.Show("Target disk not found", "Error");
                        return;
                    }
                }
                DisableUI();
                Thread thread = new Thread(delegate()
                {
                    m_isBusy = true;
                    CopyDisk(sourceDisk, targetDisk);
                    m_isBusy = false;
                    if (m_isClosing)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Close();
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            EnableUI();
                        });
                    }
                });
                thread.Start();
            }
        }

        private void EnableUI()
        {
            btnStart.Text = "Start";
            comboSourceDisk.Enabled = true;
            comboTargetDisk.Enabled = true;
            groupOptions.Enabled = true;
        }

        private void DisableUI()
        {
            btnStart.Text = "Stop";
            comboSourceDisk.Enabled = false;
            comboTargetDisk.Enabled = false;
            groupOptions.Enabled = false;
        }

        private void CopyDisk(Disk sourceDisk, Disk targetDisk)
        {
            if (sourceDisk.BytesPerSector != targetDisk.BytesPerSector)
            {
                MessageBox.Show("The target disk has different sector size than the source disk", "Error");
                return;
            }

            if (sourceDisk.Size > targetDisk.Size)
            {
                DialogResult partialCopyDialogResult = MessageBox.Show("The target disk is smaller than the source disk, Do you wish to perform a partial copy?", "Warning", MessageBoxButtons.YesNo);
                if (partialCopyDialogResult == DialogResult.No)
                {
                    return;
                }
            }

            if (targetDisk is PhysicalDisk)
            {
                DialogResult dialogResult = MessageBox.Show("The copy operation will erase all existing data on the selected disk, Are you sure?", "Warning", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            m_startTime = DateTime.Now;
            ClearLog();
            AddToLog("Raw Disk Copier {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3));
            if (sourceDisk is PhysicalDisk)
            {
                AddToLog("Source disk: {0}, S/N: {1}", ((PhysicalDisk)sourceDisk).Description, ((PhysicalDisk)sourceDisk).SerialNumber);
            }
            else if (targetDisk is VirtualHardDisk)
            {
                AddToLog("Target disk: {0}", ((VirtualHardDisk)sourceDisk).Path);
            }

            if (targetDisk is PhysicalDisk)
            {
                AddToLog("Target disk: {0}, S/N: {1}", ((PhysicalDisk)targetDisk).Description, ((PhysicalDisk)targetDisk).SerialNumber);
            }
            else if (targetDisk is VirtualHardDisk)
            {
                AddToLog("Target disk: {0}", ((VirtualHardDisk)targetDisk).Path);
            }
            AddToLog("Source disk size: {0} ({1:###,###,###,###,##0} sectors, {2} bytes per sector)", UIHelper.GetSizeString(sourceDisk.Size), sourceDisk.TotalSectors, sourceDisk.BytesPerSector);
            AddToLog("Target disk size: {0} ({1:###,###,###,###,##0} sectors, {2} bytes per sector)", UIHelper.GetSizeString(targetDisk.Size), targetDisk.TotalSectors, targetDisk.BytesPerSector);

            if (targetDisk is PhysicalDisk)
            {
                bool success = ((PhysicalDisk)targetDisk).ExclusiveLock();
                if (!success)
                {
                    MessageBox.Show("Failed to lock the target disk.");
                    return;
                }

                if (Environment.OSVersion.Version.Major >= 6)
                {
                    success = ((PhysicalDisk)targetDisk).SetOnlineStatus(false, false);
                    if (!success)
                    {
                        ((PhysicalDisk)targetDisk).ReleaseLock();
                        MessageBox.Show("Failed to take the target disk offline.");
                        return;
                    }
                }
            }
            else if (targetDisk is VirtualHardDisk)
            {
                ((VirtualHardDisk)targetDisk).ExclusiveLock();
            }

            m_diskCopier = new DiskCopier(sourceDisk, targetDisk);
            m_diskCopier.OnStatusUpdate += delegate(long currentPosition, long numberOfUnrecoveredSectors)
            {
                UpdateStatus(currentPosition, numberOfUnrecoveredSectors);
            };
            m_diskCopier.OnLogUpdate += delegate(string format, object[] args)
            {
                AddToLog(format, args);
            };
            // The last segment might be bigger than the others
            long totalSectors = Math.Min(sourceDisk.TotalSectors, targetDisk.TotalSectors);
            long uiBlockSize = totalSectors / NumberOfUIBlocks;
            m_blocks = new BlockStatus[NumberOfUIBlocks];
            this.Invoke((MethodInvoker)delegate
            {
                lblUnrecoveredSectors.Text = "Unrecovered Sectors: 0";
                pictureBoxMap.Invalidate();
                pictureBoxMap.Update();
            });
            for (int uiBlockIndex = 0; uiBlockIndex < m_blocks.Length; uiBlockIndex++)
            {
                long sectorIndex = uiBlockIndex * uiBlockSize;
                long sectorCount = uiBlockSize;
                if (uiBlockIndex == m_blocks.Length - 1)
                {
                    sectorCount = totalSectors - ((m_blocks.Length - 1) * uiBlockSize);
                }

                BlockStatus blockStatus;
                if (chkMaximizeDataRecovery.Checked)
                {
                    blockStatus = m_diskCopier.ThroughCopy(sectorIndex, sectorCount, chkWriteZeros.Checked);
                }
                else
                {
                    blockStatus = m_diskCopier.QuickCopy(sectorIndex, sectorCount, chkWriteZeros.Checked);
                }
                if (blockStatus == BlockStatus.WriteError)
                {
                    MessageBox.Show("Failed to write to the target disk", "Write Error");
                    break;
                }
                m_blocks[uiBlockIndex] = blockStatus;
                this.Invoke((MethodInvoker)delegate
                {
                    pictureBoxMap.Invalidate();
                    pictureBoxMap.Update();
                });
                if (m_diskCopier.Abort)
                {
                    break;
                }
            }

            if (targetDisk is PhysicalDisk)
            {
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    ((PhysicalDisk)targetDisk).SetOnlineStatus(true, false);
                }
                ((PhysicalDisk)targetDisk).ReleaseLock();
                if (chkInvalidateWindowsCache.Checked)
                {
                    ((PhysicalDisk)targetDisk).UpdateProperties();
                }
            }
            else if (targetDisk is VirtualHardDisk)
            {
                ((VirtualHardDisk)targetDisk).ReleaseLock();
            }

            if (m_diskCopier.Abort)
            {
                AddToLog("Copy Aborted");
            }
            else
            {
                AddToLog("Copy Completed");
            }
            m_diskCopier = null;
        }

        private void UpdateStatus(long currentPosition, long numberOfUnrecoveredSectors)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateStatus(currentPosition, numberOfUnrecoveredSectors);
                });
            }
            else
            {
                int totalSeconds = (int)((TimeSpan)(DateTime.Now - m_startTime)).TotalSeconds;
                totalSeconds = Math.Max(totalSeconds, 1);
                long speed = currentPosition / totalSeconds;
                lblSpeed.Text = String.Format("Speed: {0}/s", UIHelper.GetSizeString(speed));
                lblPosition.Text = String.Format("Position: {0:###,###,###,###,##0}", currentPosition);
                lblUnrecoveredSectors.Text = String.Format("Unrecovered Sectors: {0:###,###,###,###,##0}", numberOfUnrecoveredSectors);
            }
        }

        private void ClearLog()
        {
            m_log = new List<string>();
        }

        private void AddToLog(string format, params object[] args)
        {
            string message = String.Format(format, args);
            AddToLog(message);
        }

        private void AddToLog(string message)
        {
            string messageFormatted = String.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
            m_log.Add(messageFormatted);
        }
    }
}