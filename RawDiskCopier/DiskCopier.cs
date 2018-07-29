/* Copyright (C) 2016-2018 Tal Aloni <tal.aloni.il@gmail.com>. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using DiskAccessLibrary;
using Utilities;

namespace RawDiskCopier
{
    public class DiskCopier : DiskReader
    {
        private Disk m_targetDisk;

        public DiskCopier(Disk sourceDisk, Disk targetDisk) : base(sourceDisk)
        {
            m_targetDisk = targetDisk;
        }

        public BlockStatus QuickCopy(long sectorIndex, long sectorCount, bool writeZeros)
        {
            bool crcErrorOccured = false;
            for (long sectorOffset = 0; sectorOffset < sectorCount; sectorOffset += PhysicalDisk.MaximumDirectTransferSizeLBA)
            {
                long leftToRead = sectorCount - sectorOffset;
                int sectorsToRead = (int)Math.Min(leftToRead, PhysicalDisk.MaximumDirectTransferSizeLBA);
                bool ioErrorOccured;
                // Clear allocations from previous iteration
                GC.Collect();
                GC.WaitForPendingFinalizers();
                byte[] data = ReadSectorsUnbuffered(sectorIndex + sectorOffset, sectorsToRead, out ioErrorOccured);
                if (Abort)
                {
                    return BlockStatus.Untested;
                }

                if (ioErrorOccured)
                {
                    return BlockStatus.IOError;
                }

                if (data == null)
                {
                    crcErrorOccured = true;
                    if (writeZeros)
                    {
                        data = new byte[sectorsToRead * m_targetDisk.BytesPerSector];
                    }
                }

                if (data != null)
                {
                    try
                    {
                        WriteSectors(sectorIndex + sectorOffset, data);
                    }
                    catch (IOException)
                    {
                        return BlockStatus.WriteError;
                    }
                }

                if (Abort)
                {
                    return BlockStatus.Untested;
                }
            }

            if (crcErrorOccured)
            {
                return BlockStatus.Damaged;
            }
            else
            {
                return BlockStatus.OK;
            }
        }

        public BlockStatus ThroughCopy(long sectorIndex, long sectorCount)
        {
            bool crcErrorOccured = false;
            for (long sectorOffset = 0; sectorOffset < sectorCount; sectorOffset += PhysicalDisk.MaximumDirectTransferSizeLBA)
            {
                long leftToRead = sectorCount - sectorOffset;
                int sectorsToRead = (int)Math.Min(leftToRead, PhysicalDisk.MaximumDirectTransferSizeLBA);
                List<long> damagedSectors;
                bool ioErrorOccured;
                // Clear allocations from previous iteration
                GC.Collect();
                GC.WaitForPendingFinalizers();
                byte[] data = ReadEverySector(sectorIndex + sectorOffset, sectorsToRead, out damagedSectors, out ioErrorOccured);
                if (Abort)
                {
                    return BlockStatus.Untested;
                }

                if (ioErrorOccured)
                {
                    return BlockStatus.IOError;
                }

                if (damagedSectors.Count > 0)
                {
                    crcErrorOccured = true;
                }

                try
                {
                    WriteSectors(sectorIndex + sectorOffset, data);
                }
                catch (IOException)
                {
                    return BlockStatus.WriteError;
                }

                if (Abort)
                {
                    return BlockStatus.Untested;
                }
            }

            if (crcErrorOccured)
            {
                return BlockStatus.Damaged;
            }
            else
            {
                return BlockStatus.OK;
            }
        }

        public void WriteSectors(long sectorIndex, byte[] data)
        {
            int sectorCount = data.Length / m_targetDisk.BytesPerSector;
            if (sectorCount > PhysicalDisk.MaximumDirectTransferSizeLBA)
            {
                // we must write one segment at the time
                for (int sectorOffset = 0; sectorOffset < sectorCount; sectorOffset += PhysicalDisk.MaximumDirectTransferSizeLBA)
                {
                    int leftToWrite = sectorCount - sectorOffset;
                    int sectorsToWrite = (int)Math.Min(leftToWrite, PhysicalDisk.MaximumDirectTransferSizeLBA);
                    byte[] segment = new byte[sectorsToWrite * m_targetDisk.BytesPerSector];
                    Array.Copy(data, sectorOffset * m_targetDisk.BytesPerSector, segment, 0, sectorsToWrite * m_targetDisk.BytesPerSector);
                    m_targetDisk.WriteSectors(sectorIndex + sectorOffset, segment);
                }
            }
            else
            {
                m_targetDisk.WriteSectors(sectorIndex, data);
            }
        }

        public Disk TargetDisk
        {
            get
            {
                return m_targetDisk;
            }
        }
    }
}
